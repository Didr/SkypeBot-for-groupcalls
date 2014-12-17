using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKYPE4COMLib;


namespace SkypeBotForm
{
    public partial class Form1 : Form
    {
        private Skype skype;
        private const string trigger = "!"; // Say !help
        private const string nick = "Botman";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            skype = new Skype();
            // Use skype protocol version 7 
            skype.Attach(7, false);
            // Listen 
            skype.MessageStatus +=
              new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
        
       }

        private void skype_MessageStatus(ChatMessage msg,
             TChatMessageStatus status)
        {
            // Proceed only if the incoming message is a trigger
            if (msg.Body.IndexOf(trigger) == 0)
            {
                // Remove trigger string and make lower case
                string command = msg.Body.Remove(0, trigger.Length).ToLower();
                textBox1.Text = msg.Chat.FriendlyName;
                textBox2.Text = msg.Chat.Name;

                string groupname2;
                string groupname;
                groupname = textBox1.Text;
                groupname2 = textBox2.Text;



                // Send processed message back to skype chat window
               // skype.SendMessage(msg.Sender.Handle, nick +
                 //     " Says: " + ProcessCommand(command));
                msg.Chat.SendMessage(" " + ProcessCommand(command));
            }

        }

        


        private string ProcessCommand(string str)
        {
            string result;
            switch (str)
            {
                case "hello":
                    result = "Hello!";
                    break;

                case "call":
                result = "Calling now!" +
                    skype.PlaceCall(textBox2.Text);
                    break;

                default:
                    result = "Sorry, I do not recognize your command. Yu dun goofd";
                    break;
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }



    }
}



// Huge props to Praveen Nair on codeproject for his example. http://www.codeproject.com/Articles/37909/Make-your-Skype-Bot-in-NET
