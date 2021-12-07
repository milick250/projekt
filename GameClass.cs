using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seminar
{
    public class ListaLikova<T> : List<T>
    {        
        public new void Add(T item)
        {
            base.Add(item);
            Change = true;
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            Change = true;
        }

        public ListaLikova()
        {
            Change = false;
        }
        
        private bool change;

        public bool Change
        {
            get { return change; }
            set { change = value; }
        }
    }

    public class Game
    {
        public static void WaitMS(int ms)
        {
            Thread.Sleep(ms);
        }

        public static void AddSprite(Svi s)
        {            
            s.SpriteIndex = BGL.spriteCount;
            BGL.spriteCount++;
            BGL.svi_likovi.Add(s);
        }

        

        public static void StartScript(Func<int> scriptName)
        {
            Task t;
            t = Task.Factory.StartNew(scriptName);
        }                        
       
    }
}
