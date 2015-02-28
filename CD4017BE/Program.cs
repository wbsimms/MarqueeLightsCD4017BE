#region copyright
// Copyright (c) 2015 Wm. Barrett Simms wbsimms.com
//
// Permission is hereby granted, free of charge, to any person 
// obtaining a copy of this software and associated documentation 
// files (the "Software"), to deal in the Software without restriction, including 
// without limitation the rights to use, copy, modify, merge, publish, 
// distribute, sublicense, and/or sell copies of the Software, 
// and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be 
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.SocketInterfaces;

namespace CD4017BE
{
    public partial class Program
    {
        private DigitalOutput output;
        private Potentiometer potentiometer;
        GT.Timer timer = new GT.Timer(200);
        void ProgramStarted()
        {
            potentiometer = new Potentiometer(13);
            output = breadBoardX1.CreateDigitalOutput(GT.Socket.Pin.Three, false);
            button.ButtonPressed += button_ButtonPressed;
            timer.Tick += timer_Tick;
            Debug.Print("Program Started");
        }

        void timer_Tick(GT.Timer timer)
        {
            int milliseconds = (int)(potentiometer.ReadProportion() * 1000);
            timer.Interval = new TimeSpan(0,0,0,0,milliseconds);
            output.Write(true);
            output.Write(false);
        }

        private bool isStarted = false;
        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            if (!isStarted)
            {
                timer.Start();
                isStarted = true;
            }
            else
            {
                timer.Stop();
                isStarted = false;
            }
        }
    }
}
