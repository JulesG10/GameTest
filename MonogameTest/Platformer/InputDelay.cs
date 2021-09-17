using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer
{
    class InputDelay
    {
        public bool stop = false;

        private float time = 0;
        private float max;
        private bool waitStop = false;

        public InputDelay(float max,bool stop = false)
        {
            this.max = max;
            this.stop = stop;
        }

        public float getTime(float delta,Func<bool> input)
        {
            if(input.Invoke())
            {
                this.time += delta * 1000;
                this.waitStop = true;
            }else if(this.waitStop)
            {
                this.waitStop = false;
                float t = this.time;
                this.time = 0;
                return t;
            }

            return -1;
        }

        public bool isActive(float delta,Func<bool> input)
        {
            
            if (input.Invoke())
            {
                this.time += delta * 1000;
                if (this.time >= this.max)
                {
                    this.time = 0;
                    if(this.stop)
                    {
                        if(this.waitStop)
                        {
                            return false;
                        }
                        this.waitStop = true;
                    }

                    return true;
                }

            }
            else
            {
                this.waitStop = false;
            }

            return false;
        }
    }
}
