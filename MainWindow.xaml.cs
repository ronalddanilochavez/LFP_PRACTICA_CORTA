using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LFP_PRACTICA_CORTA;

namespace LFP_PRACTICA_CORTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAnalize_Click(object sender, RoutedEventArgs e)
        {
            ArrayList tokens = new ArrayList();
            string token = "";
            string sentence = TextIntro.Text;
            bool endOfSentence = true;  // First we assume true if we are not began some leter
            TextInfo.Text = "";

            TextInfo.AppendText("TOKENS");

            for (int i = 0; i < sentence.Length; i++)
            {
                if (!sentence[i].Equals(' '))
                {
                    token += sentence[i].ToString();
                    endOfSentence = false;

                    if (i == sentence.Length - 1)
                    {
                        tokens.Add(token);
                        writeToken(token);
                        token = "";
                        endOfSentence = true;
                    }
                }
                else
                {
                    if (endOfSentence == false)
                    {
                        tokens.Add(token);
                        writeToken(token);
                        token = "";
                        endOfSentence = true;
                    }
                }
            }
        }

        public void writeToken(string token)
        {
            if (isMoneySimbolWithDecimal(token))
            {
                TextInfo.AppendText("\n [" + token + "] es Moneda con Decimal");
            }
            else if (isMoneySimbol(token))
            {
                TextInfo.AppendText("\n [" + token + "] es Moneda");
            }
            else if (isInteger(token))
            {
                TextInfo.AppendText("\n [" + token + "] es Entero");
            }
            else if (isDecimal(token))
            {
                TextInfo.AppendText("\n [" + token + "] es Decimal");
            }
            else if (isWord(token))
            {
                TextInfo.AppendText("\n [" + token + "] es Palabra");
            }
        }

        public bool isWord (String token)
        {
            return !token.StartsWith("0") && !token.StartsWith("1") && !token.StartsWith("2") && !token.StartsWith("3") && !token.StartsWith("4") && !token.StartsWith("5") && !token.StartsWith("6") && !token.StartsWith("7") && !token.StartsWith("8") && !token.StartsWith("9") && !token.StartsWith("Q");
        }

        public bool isInteger(String token)
        {
            bool isInteger = false;

            for (int i = 0; i < token.Length; i++)
            {
                if (token[i].Equals('0') || token[i].Equals('1') || token[i].Equals('2') || token[i].Equals('3') || token[i].Equals('4') || token[i].Equals('5') || token[i].Equals('6') || token[i].Equals('7') || token[i].Equals('8') || token[i].Equals('9'))
                {
                    isInteger = true;
                }
                else 
                {
                    isInteger = false;
                    break;
                }
            }

            return isInteger;
        }

        public bool isDecimal(string token) 
        {
            bool isDecimal = false;
            /*String integerPart = "";
            String fractionalPart = "";
            bool fractionalTurn = false;*/
            bool isFirstTime = false;

            if (token.Length > 1)
            {
                for (int i = 0; i < token.Length; i++)
                {

                    if (token[i].Equals('0') || token[i].Equals('1') || token[i].Equals('2') || token[i].Equals('3') || token[i].Equals('4') || token[i].Equals('5') || token[i].Equals('6') || token[i].Equals('7') || token[i].Equals('8') || token[i].Equals('9') || token[i].Equals('.'))
                    {
                        if (token[i].Equals('.'))
                        {
                            //fractionalTurn = true;
                            isFirstTime = !isFirstTime;  // To ensure we have only one point in expression to be decimal
                            if (isFirstTime == false)
                            {
                                isDecimal = false;
                                break;
                            }
                        }
                        else
                        {
                            isDecimal = true;
                        }
                    }
                    else
                    {
                        isDecimal = false;
                        return isDecimal;
                    }
                }
            }

            return isDecimal;
        }

        public bool isMoneySimbol(string token)
        {
            bool isMoneySimbol = false;

            if (token.Equals("Q") || token.Equals("$") || token.Equals("€"))
            {
                isMoneySimbol = true;
                return isMoneySimbol;
            }

            if (token.Equals("Q.") || token.Equals("$.") || token.Equals("€."))
            {
                isMoneySimbol = true;
                return isMoneySimbol;
            }

            return isMoneySimbol;
        }

        public bool isMoneySimbolWithDecimal(string token)
        {
            bool isMoneySimbolWithDecimal = false;

            string removeFirstCharacter (string myString)
            {
                string temp = "";

                if (token.Length >= 2)
                {
                    for (int i = 1; i < myString.Length; i++)
                    {
                        temp += myString[i];
                    }
                }

                return temp;
            }

            // We search for the structure "Q. 200.0" or "Q 200.0"
            if ((token[0].Equals('Q') || token[0].Equals('$') || token[0].Equals('€')) && token.Length > 1)
            {
                if (token[1].Equals('.') && token.Length > 2)
                {
                    if (token[2].Equals(' ') && token.Length > 3) // We search for the structure "Q. 200.0"
                    {
                        token = removeFirstCharacter(token); // Removes Q
                        token = removeFirstCharacter(token); // Removes .
                        token = removeFirstCharacter(token); // Removes " "
                        if (isDecimal(token))
                        {
                            isMoneySimbolWithDecimal = true;
                            return isMoneySimbolWithDecimal;
                        }
                        else
                        {
                            isMoneySimbolWithDecimal = false;
                            return isMoneySimbolWithDecimal;
                        }
                    }
                    else // We search for the structure "Q.200.0"
                    {
                        token = removeFirstCharacter(token); // Removes Q
                        token = removeFirstCharacter(token); // Removes .
                        if (isDecimal(token))
                        {
                            isMoneySimbolWithDecimal = true;
                            return isMoneySimbolWithDecimal;
                        }
                        else
                        {
                            isMoneySimbolWithDecimal = false;
                            return isMoneySimbolWithDecimal;
                        }
                    }
                }
                else if (token.Length > 2)
                {
                    if (token[1].Equals(' ')) // We search for the structure "Q 200.0"
                    {
                        token = removeFirstCharacter(token); // Removes Q
                        token = removeFirstCharacter(token); // Removes " "
                        if (isDecimal(token))
                        {
                            isMoneySimbolWithDecimal = true;
                            return isMoneySimbolWithDecimal;
                        }
                        else
                        {
                            isMoneySimbolWithDecimal = false;
                            return isMoneySimbolWithDecimal;
                        }
                    }
                    else // We search for the structure "Q200.0"
                    {
                        token = removeFirstCharacter(token); // Removes Q
                        if (isDecimal(token))
                        {
                            isMoneySimbolWithDecimal = true;
                            return isMoneySimbolWithDecimal;
                        }
                        else
                        {
                            isMoneySimbolWithDecimal = false;
                            return isMoneySimbolWithDecimal;
                        }
                    }
                }
            }

            return isMoneySimbolWithDecimal;
        }

        private void TextInfo_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }

}
