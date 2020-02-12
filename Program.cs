using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace object_oriented_queue
{
    //Program
    internal class Program
    {
        //abstract data type class
        private class ADT
        {
            //decalrations
            public int memorysize;
            public int front;
            public int rear;
            public string[] memory;
            public int MemoryStart = 0;
            
            //constructor
            public ADT(int newmemorysize)
            {
                memorysize = newmemorysize;
            }
            
            //create memory
            public Array CreateMemory(int size)
            {
                memory = new string[size];
                front = 0;
                rear = 0;
                return memory;
            }

            //isEmpty
            public bool isEmpty()
            {
                int sum = 0;
                
                //check for empty memory
                for (var i = 0; i < memorysize; i++)
                {
                    if (memory[i] == null)
                    {
                        sum += 1;
                        
                    }
                }

                if (sum == memorysize)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            //isFull
            public bool isFull()
            {
                int sum = 0;

                //check for full memory
                for (var i = 0; i < memorysize; i++)
                {
                    if (memory[i] != null)
                    {
                        sum += 1;
                    }
                }

                if (sum == memorysize)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            //return memory
            public void show()
            {
                for (int i = 0; i < memorysize; i++)
                {
                    Console.WriteLine("index: " + i + " = " + memory[i]);
                }
            }
            
            //size
            public int Size()
            {
                int count = 0;
                for (int i = 0; i < memorysize; i++)
                {
                    if (memory[i] != null)
                    {
                        count += 1;
                    }
                }

                return count;
            }
        }
        
        //stack
        private class Stack : ADT
        {
            //constructor
            public Stack(int newmemorysize) : base(newmemorysize)
            {
                CreateMemory(memorysize);
            }
            
            //pop
            public string pop()
            {
                return this.peek(true);
            }
            
            //peek
            public string peek(bool pop)
            {
                if (this.isEmpty() == true)
                {
                    return "Stack is empty...";
                }

                string popitem;

                for (int i = memorysize - 1; i > 0; i--)
                {
                    if (memory[i] != null)
                    {
                        popitem = memory[i];
                        
                        if (pop)
                        {
                            memory[i] = null;
                        }

                        return popitem;
                    }
                }

                return "Stack is empty...";
            }
            
            //push
            public void push(string newitem)
            {
                if (this.isFull() == true)
                {
                    Console.WriteLine("Stack is full...");
                    return;
                }
                
                for (int i = 0; i < memorysize; i++)
                {
                    if (memory[i] == null)
                    {
                        memory[i] = newitem;
                        return;
                    }
                }
            }
            
        }
        
        //inherited queue class
        private class LinearQueue : ADT
        {
            private bool isCirular = false;
            private bool isPriority = false;
            
            //constructor
            public LinearQueue(int newmemorysize) : base(newmemorysize)
            {
                CreateMemory(memorysize);
            }
            
            //enQueue
            public virtual void enQueue(string newItem)
            {
                //if start
                if (rear == front)
                {
                    memory[rear] = newItem;
                    rear += 1;
                }
                //queue full
                else if (rear == memorysize)
                {
                    Console.WriteLine("\n Reached end of queue, do you with to rewrite? (Y/N)\n");
                    if (Console.ReadLine() == "Y")
                    {
                        memory[rear] = newItem;
                    }
                }
                else
                {
                    memory[rear] = newItem;
                    rear += 1;
                }
            }
            
            //deQueue
            public virtual void deQueue()
            {
                //check empty
                if (isEmpty())
                {
                    Console.WriteLine("\n queue is empty, there are no elements to return\n");
                    return;
                }
                
                //hold item
                string item = memory[front];
                
                //remove front item
                memory[front] = null;
                
                //shift pointer
                front += 1;
            }
            
            //isCircular
            public bool is_Circular()
            {
                return isCirular;
            }
            
            //isPriority
            public bool is_Priority()
            {
                return isPriority;
            }
            
            //setCircular
            public void setCircular(bool set)
            {
                isCirular = set;
            }
            
            //setPriority
            public void setPriority(bool set)
            {
                isPriority = set;
            }

        }
        
        //polymorphism circular queue
        private class CircularQueue : LinearQueue
        {
            //constructor
            public CircularQueue(int newmemorysize) : base(newmemorysize)
            {
                CreateMemory(memorysize);
                setCircular(true);
            }
            
            //enQueue
            public override void enQueue(string newItem)
            {
                //reach end of queue
                if (rear == memorysize)
                {
                    if (memory[MemoryStart] != null)
                    {
                        Console.WriteLine("\n There is no empty space, do you with to rewrite? (Y/N)\n");
                        if (Console.ReadLine() == "Y")
                        {
                            rear = MemoryStart;
                            memory[rear] = newItem;
                            rear += 1;
                        } 
                    }
                    else
                    {
                        rear = MemoryStart;
                        memory[rear] = newItem;
                        rear += 1;
                    }
                    
                }
                else
                {
                    if (memory[rear] != null)
                    {
                        Console.WriteLine("\n There is no empty space, do you with to rewrite? (Y/N)\n");
                        if (Console.ReadLine() == "Y")
                        {
                            memory[rear] = newItem;
                            rear += 1; 
                        }
                    }
                    else
                    {
                        memory[rear] = newItem;
                        rear += 1;
                    }
                }
            }
            
            //deQueue
            public override void deQueue()
            {
                //check empty
                if (isEmpty())
                {
                    Console.WriteLine("\n queue is empty, there are no elements to return\n");
                    return;
                }
                
                //hold item
                string item = memory[front];
                
                //remove front item
                memory[front] = null;
                
                //shift pointer
                if (front == memorysize)
                {
                    front = MemoryStart;
                }
                else
                {
                    front += 1; 
                }
                Console.WriteLine(front + " dequeue front");
            }
        }

        //entry point
        public static void Main(string[] args)
        {
            //linear Queue
            Console.WriteLine("Linear queue: ");
            var q = new LinearQueue(6);
            Console.WriteLine("is empty = " + q.isEmpty().ToString());
            q.enQueue("orange");
            q.enQueue("red");
            q.enQueue("blue");
            q.enQueue("orange");
            q.enQueue("red");
            q.enQueue("blue");
            q.show();
            Console.WriteLine(q.isFull().ToString());
            q.deQueue();
            q.show();
            Console.WriteLine(q.isFull().ToString());

            //circular queue
            Console.WriteLine("\n\nCircular Queue");
            var q2 = new CircularQueue(4);
            Console.WriteLine("is empty = " + q2.isEmpty().ToString());
            q2.enQueue("orange");
            q2.enQueue("red");
            q2.show();
            q2.deQueue();
            q2.show();
            q2.enQueue("red");
            q2.enQueue("blue");
            q2.enQueue("orange");
            q2.enQueue("orange");
            q2.show();
            
            //stack
            Console.WriteLine("\n\nStack");
            var s = new Stack(5);
            Console.WriteLine("is empty: " + s.isEmpty().ToString());
            s.push("red");
            s.push("green");
            s.push("Blue");
            s.show();
            s.peek(false);
            s.pop();
            s.push("Orange");
            s.push("Indigo");
            s.push("Violet");
            Console.WriteLine("\nis full: " + s.isFull().ToString());
            s.show();

        }
    }
}