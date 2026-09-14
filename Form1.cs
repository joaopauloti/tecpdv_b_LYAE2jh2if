using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistemaTecPDV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (SistemaTecPDV.Dados.ConexaoBanco
                    .TestarConexao(out string msg))
            {
                MessageBox.Show(msg, "Teste de Conexão",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(msg, "Teste de Conexão",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
