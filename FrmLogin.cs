using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using SistemaTecPDV.Dados;
 
namespace SistemaTecPDV
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        // ↓↓↓ COLE A PARTIR DE AQUI ↓↓↓

        private string HashSenha(string senha)
        {
            byte[] bytes = SHA256.HashData(
                Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(bytes);
        }
        private void btnEntrar_Click(
            object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text;

            // Nenhum campo pode ficar vazio
            if (string.IsNullOrEmpty(usuario) ||
                string.IsNullOrEmpty(senha))
            {
                MessageBox.Show(
                    "Preencha usuário e senha.",
                    "Login", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string senhaHash = HashSenha(senha);
            try
            {
                using var conexao =
                    ConexaoBanco.ObterConexao();
                conexao.Open();

                var cmd = conexao.CreateCommand();
                cmd.CommandText =
                    "SELECT Nome FROM Usuario " +
                    "WHERE Login=@login AND " +
                    "SenhaHash=@senhaHash;";
                cmd.Parameters.AddWithValue(
                    "@login", usuario); 
                cmd.Parameters.AddWithValue(
                    "@senhaHash", senhaHash);
                var resultado = cmd.ExecuteScalar();


                if (resultado != null)
                {

                    // + a partir de agora, abrimos o Cadastro de
                    // + Produtos e escondemos a tela de login:
                    //this.Hide();
                    //var telaProdutos = new FrmProdutos();
                    //telaProdutos.Show();
                    MessageBox.Show($"Bem-vindo(a), {resultado}!");

                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos.");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }  // fim do btnEntrar_Click

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}