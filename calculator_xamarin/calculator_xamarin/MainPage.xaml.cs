using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using AngouriMath;

namespace calculator_xamarin
{
    public partial class MainPage : ContentPage
    {

        private string AppTheme;
        String current_opt = "";

        public MainPage()
        {
            InitializeComponent();


            Xamarin.Essentials.AppTheme theme = AppInfo.RequestedTheme;
            switch (theme)
            {
                case Xamarin.Essentials.AppTheme.Dark:
                    AppTheme = "dark";
                    break;
                case Xamarin.Essentials.AppTheme.Light:
                    AppTheme = "light";
                    break;
                default:
                    AppTheme = "light";
                    break;
            }
            if(AppTheme == "light")
            {
                Main_number_add.TextColor = Color.Black;
            }
        }

        private char LastChar()
        {
            return current_opt[current_opt.Length - 1];
        }

        private Boolean isOnOperation()
        {
            char l = LastChar();
            return l == '+' || l == '-' || l == '÷' || l == '×' || l == '.';
        }

        private Boolean isClosingBracket()
        {
            return LastChar() == ')';
        }

        
        private void Equal(object sender, EventArgs args)
        {

            try {
                Entity expr = current_opt;
                double output = (double)expr.EvalNumerical();
                if(output%1 == 0)
                {
                    int o = Convert.ToInt32(output);
                    Convert.ToString(Convert.ToString(o));
                }
                else
                {
                    setNShow(Convert.ToString(output));
                }
            }
            catch
            {
                setNShow("error");
            }
        }

        private void Bracket(object sender, EventArgs args)
        {
            if (LastChar() == ')' && !isOnOperation())
            {
                Button btn = (Button)Sender;
                string btn_text = (String)btn.BindingContext;
                addNShow(btn_text);
            }
        }

        private void addNShow(String btnText)
        {
            current_opt += btnText;
            Main_number_add.Text = current_opt + " ";
        }

        private void setNShow(String btnText)
        {
            current_opt = btnText;
            Main_number_add.Text = current_opt + " ";
        }

        private void addOperation(object sender, EventArgs args)
        {
            if (!isOnOperation()){
                Button btn = (Button)Sender;
                string btn_text = (String)btn.BindingContext;

                if(!((btn_text == "/" || btn_text == "*") && LastChar() == '('))
                {
                    addNShow(btn_text);
                }
            }

        }

        private void addNumber(object sender, EventArgs args)
        {
            Button btn = (Button)Sender;
            string btn_text = (String)btn.BindingContext;

            if (!isClosingBracket() && LastChar() != '.')
            {
                addNShow(btn_text);
            }

        }

        private void Clean(object sender, EventArgs args)
        {
            current_opt = "";
        }

    }
}
