using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextAnalyzer
{
    public partial class Form1 : Form
    {
        String text = " ";
        int l = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            text = textBox1.Text;
            l = text.Length;
            if (l > 0)
                AnalyzeText();
            else MessageBox.Show("Syötä analysoitava teksti");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Boolean EndOfSentence(String st)
        {
            return (st.Equals(".") || st.Equals("!") || st.Equals("?"));
        }

        private void AnalyzeText()
        {
            //MessageBox.Show("Analysoidaan teksti");
            int i;
            double senu = 0.0; //Virkkeiden määrä
            double wonu = 0.0; //Sanojen määrä
            int sewonu = 0; //Virkkeen sanojen määrä
            double awle = 0.0; //Virkkeen keskipituus
            int min = 0; //Lyhyimmän virkkeen pituus
            int max = 0; //Pisimmän virkkeen pituus
            string word_info = " ";
            string sentence_info = " ";
            string awle_info = " ";
            string max_info = " ";
            string min_info = " ";

            char c, cprev;
            string s, sprev;
            Boolean between_sentences;
           
            for (i = 0; i<l; i++)
            {
                c = text[i];
                s = (Char.ToString(c));
                if (s.Equals(" "))
                //Lisää sanojen ja virkkeen sanojen määrää, jos edessä ei ole virkkeen lopetusmerkki.
                {
                    between_sentences = false;
                    if(i<0)
                    {
                        cprev = text[i-1];
                        sprev = (Char.ToString(cprev));
                        between_sentences = EndOfSentence(sprev);
                        //if (sprev.Equals(".") || sprev.Equals("!") || sprev.Equals("?"))
                            //between_sentences = true;
                    }
                    if (!(between_sentences))
                    { 
                        wonu++;
                        sewonu++;
                    }
                }
                else if (EndOfSentence(s))
                {
                    /*Virkkeen lopetusmerkki lisää virkkeiden määrää.
                     Jos 1. virke, myös käsitellyn virkkeen sanojen määrää pitää lisätä.
                     Vaihtaa tarvittaessa virkkeen sanojen maksimin tai minimin ja
                     nollaa virkkeen sanat seuraavan virkkeen käsittelyä varten.
                     */
                    senu++;
                    if(senu==1)
                        sewonu++;
                    if ((min == 0) || (sewonu < min))//On käyty läpi 1. virke tai on löytynyt entistä lyhyempi virke
                        min = sewonu;
                   // MessageBox.Show("Minimi saa arvon " + min);
                    if ((max == 0) || (sewonu > max))//On käyty läpi 1. virke tai on löytynyt entistä pitempi virke
                        max = sewonu;
                    //MessageBox.Show("Maksimi saa arvon " + max);
                    sewonu = 0;
                }
            }
            wonu++;
            sentence_info = ("Virkkeitä löytyi " + senu+". ");
            word_info = ("Sanoja löytyi " + wonu + ". ");
            awle = Math.Round(wonu / senu);
            //;
            awle_info = ("Virkkeen keskipituus on " + awle + " sanaa. ");
            max_info = ("Pisimmässä virkkeessä oli sanoja " + max+". ");
            min_info = ("Lyhimmmässä virkkeessä oli sanoja " + min+".");
            MessageBox.Show(sentence_info + word_info + awle_info + max_info  +min_info);
            textBox1.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
