﻿using Hospital_Management.Modelo_de_datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_Management.Vistas
{
    public partial class Historial : Form
    {
        public Historial()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            Controles controles = new Controles();
            controles.Show();
        }

        private void Historial_Load(object sender, EventArgs e)
        {
            ActualizarDataGridView();
        }

        public void ActualizarDataGridView()
        {
            FileStream fs = null;
            BinaryReader reader = null;

            try
            {
                if (!File.Exists("registros.bin"))
                {
                    dgvHistorial.DataSource = null;
                    MessageBox.Show("No se encontraron registros guardados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                fs = new FileStream("registros.bin", FileMode.Open);
                reader = new BinaryReader(fs);

                List<Registro> registros = new List<Registro>();

                while (fs.Position < fs.Length)
                {
                    string id = reader.ReadString();
                    string nombre = reader.ReadString();
                    string direccion = reader.ReadString();
                    string contacto = reader.ReadString();
                    string edad = reader.ReadString();
                    string genero = reader.ReadString();
                    string tipoSangre = reader.ReadString();
                    string enfermedadAnterior = reader.ReadString();
                    string sintomas = reader.ReadString();
                    string diagnostico = reader.ReadString();
                    string medicamentos = reader.ReadString();
                    string requerimientodesala = reader.ReadString();
                    string tipodesala = reader.ReadString();

                    registros.Add(new Registro(id, nombre, direccion, contacto, edad, genero, tipoSangre,
                        enfermedadAnterior, sintomas, diagnostico, medicamentos, requerimientodesala, tipodesala));
                }

                dgvHistorial.DataSource = registros;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el DataGridView: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (fs != null) fs.Close();
            }
        }

        private void dgvHistorial_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvHistorial_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvHistorial_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (dgvHistorial.SelectedRows.Count > 0)
            {
                int rowIndex = dgvHistorial.SelectedRows[0].Index;

                Registro registroSeleccionado = (Registro)dgvHistorial.Rows[rowIndex].DataBoundItem;

                EditarRegistroForm frmEditar = new EditarRegistroForm(registroSeleccionado);

                if (frmEditar.ShowDialog() == DialogResult.OK)
                {
                    ActualizarDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDataGridView();
        }
    }   

}


