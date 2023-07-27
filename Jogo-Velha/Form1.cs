using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_Velha
{
    public partial class Form1 : Form
    {
        Image ardina = Properties.Resources.Ardina;

        bool pc = false;
        bool turno = true;// true = X turno; false = O turno
        int contagemTurno = 0;
        //static String jogador1, jogador2;
        public Form1()
        {
            InitializeComponent();
        }

        //public static void setNome(String n1, String n2)
        //{
        //    jogador1 = n1;
        //    jogador2 = n2;
        //}
        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Por Kauã Freitas", "Sobre", MessageBoxButtons.OK);
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turno)
            {
                b.Text = "X";

            }
            else
            {
                b.Text = "O";

            }

            turno = !turno;
            b.Enabled = false;
            contagemTurno++;

            verificarVencedor();


            if ((!turno) && (pc))

            {
                pcTurno();
            }



        }




        private void verificarVencedor()
        {
            bool temVencedor = false;
            //horizontal
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
                temVencedor = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                temVencedor = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                temVencedor = true;

            //vertical
            if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled))
                temVencedor = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled))
                temVencedor = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled))
                temVencedor = true;

            //diagonal
            if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled))
                temVencedor = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!C1.Enabled))
                temVencedor = true;

            if (temVencedor)
            {
                desabilitaBotao();

                string vencedor = "";
                string ardina = "Ardina";
                if (turno)
                {
                    vencedor = p2.Text;
                    O_count.Text = (Int32.Parse(O_count.Text) + 1).ToString();
                }
                else
                {
                    vencedor = p1.Text;
                    x_count.Text = (Int32.Parse(x_count.Text) + 1).ToString();
                }
                MessageBox.Show(vencedor + " Venceu!", "Yay!");
                novoJogoToolStripMenuItem.PerformClick();

                if (vencedor == ardina)
                {
                    //Form2 form2 = new Form2();
                    //form2.Show();
                }
            }
            else
            {
                if (contagemTurno == 9)
                {
                    draw_count.Text = (Int32.Parse(draw_count.Text) + 1).ToString();
                    MessageBox.Show("Deu velha!", "Empate!");
                    novoJogoToolStripMenuItem.PerformClick();
                }
            }
        }
        private void desabilitaBotao()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }

        }

        private void novoJogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turno = true;
            contagemTurno = 0;


            foreach (Control c in Controls)
            {
                try
                {

                    Button b = (Button)c;
                    b.BackColor = Color.White;
                    b.Enabled = true;
                    b.Text = "";

                }
                catch { }
            }

        }

        private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turno)
                {
                    b.ForeColor = Color.Red;
                    b.Text = "X";
                }
                else
                {
                    b.ForeColor = Color.DarkBlue;
                    b.Text = "O";
                }
            }
        }


        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled) 
            {
                b.Text = "";
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            O_count.Text = "0";
            x_count.Text = "0";
            draw_count.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Form2 nome = new Form2();
            //nome.ShowDialog();
            //label1.Text = jogador1;
            //label3.Text = jogador2;

        }

        private void p2_TextChanged(object sender, EventArgs e)
        {
            if (p2.Text == "Computador")
                pc = true;
            else
                pc = false;
        }


        private void pcTurno()
        {


            Button move = null;

            //Oportunidades de vitoria
            move = procuravitoria("O"); //procura vitoria
            if (move == null)
            {
                move = procuravitoria("X"); //procura blocos
                if (move == null)
                {
                    move = procuracanto();
                    if (move == null)
                    {
                        move = procuralugaraberto();
                    }
                }
            }

            move.PerformClick();
        }

        private Button procuralugaraberto()
        {
            Console.WriteLine("Procurando um espaço aberto");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }
            }

            return null;
        }

        private Button procuracanto()
        {
            Console.WriteLine("procurando canto");
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }

        private Button procuravitoria(string mark)
        {
            Console.WriteLine("Procurando vitória ou bloqueio: " + mark);
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }

        private void computadorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            p2.Text = "Computador";
        }

        private void jogadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p2.Text = "Jogador 2";
        }
    }
}
