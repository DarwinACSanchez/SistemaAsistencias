using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistemaAsistencias.Datos;
using SistemaAsistencias.Logica;

namespace SistemaAsistencias.Presentacion
{
    public partial class Personal : UserControl
    {
        public Personal()
        {
            InitializeComponent();
        }
        int IdCargo;

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            LocalizarDtvCargos();
            panelCargos.Visible = false;
            panelPaginado.Visible = false;
            panelRegistros.Visible = true;
            panelRegistros.Dock = DockStyle.Fill;
            btnGuardarPersonal.Visible = true;
            btnGuardarCambiosPersonal.Visible = false;
            Limpiar();
        }
        private void LocalizarDtvCargos()
        {
            datalistadoCargos.Location = new Point(txtSueldoHora.Location.X, txtSueldoHora.Location.Y);
            datalistadoCargos.Size = new Size(469, 141);
            datalistadoCargos.Visible = true;
            lblsueldo.Visible = false;
            PanelBtnGuardarPer.Visible = false;
        }
        private void Limpiar()
        {
            txtNombres.Clear();
            txtIdentificacion.Clear();
            txtCargo.Clear();
            txtSueldoHora.Clear();
            BucarCargos();
        }

        private void btnGuardarPersonal_Click(object sender, EventArgs e)
        {

        }
        private void Insertar_Personal()
        {
            Lpersonal parametros = new Lpersonal();
            Dpersonal funcion = new Dpersonal();
            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = cbxPais.Text;
        }
        private void InsertarCargos()
        {
            if (!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoG.Text))
                {
                    Lcargos parametros = new Lcargos();
                    Dcargos funcion = new Dcargos();
                    parametros.Cargo = txtCargoG.Text;
                    parametros.SueldoPorHora = Convert.ToDouble(txtSueldoG.Text);
                    if (funcion.insertar_Cargo(parametros) == true)
                    {
                        txtCargo.Clear();
                        BucarCargos();
                        panelCargos.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agrege el sueldo", "Falta el sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agrege el cargo", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BucarCargos()
        {
            DataTable dt = new DataTable();
            Dcargos funcion = new Dcargos();
            funcion.buscarCargos(ref dt, txtCargo.Text);
            datalistadoCargos.DataSource = dt;
            Bases.DiseñoDtv(ref datalistadoCargos);
            datalistadoCargos.Columns[1].Visible = false;
            datalistadoCargos.Columns[3].Visible = false;
            datalistadoCargos.Visible = true;
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            BucarCargos();
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            panelCargos.Visible = true;
            panelCargos.Dock = DockStyle.Fill;
            panelCargos.BringToFront();
            btnGuardarC.Visible = true;
            btnGuardarCambiosC.Visible = false;
            txtCargoG.Clear();
            txtSueldoG.Clear();
        }

        private void btnGuardarC_Click(object sender, EventArgs e)
        {
            InsertarCargos();
        }

        private void txtSueldoG_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoG, e);
        }

        private void txtSueldoHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoHora, e);
        }

        private void datalistadoCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == datalistadoCargos.Columns["EditarC"].Index)
            {
                ObtenerCargoEditar();
            }
            if (e.ColumnIndex == datalistadoCargos.Columns["Cargo"].Index)
            {
                ObtenerDatosCargos();
            }
        }
        private void ObtenerDatosCargos()
        {
            IdCargo = Convert.ToInt32(datalistadoCargos.SelectedCells[1].Value);
            txtCargo.Text = datalistadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoHora.Text = datalistadoCargos.SelectedCells[3].Value.ToString();
            datalistadoCargos.Visible = false;
            PanelBtnGuardarPer.Visible = true;
            lblsueldo.Visible = true;
        }
        private void ObtenerCargoEditar()
        {
            IdCargo = Convert.ToInt32(datalistadoCargos.SelectedCells[1].Value);
            txtCargoG.Text = datalistadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoG.Text = datalistadoCargos.SelectedCells[3].Value.ToString();
            btnGuardarC.Visible = false;
            btnGuardarCambiosC.Visible = true;
            txtCargoG.Focus();
            txtCargoG.SelectAll();
            panelCargos.Visible = true;
            panelCargos.Dock = DockStyle.Fill;
            panelCargos.BringToFront();
        }

        private void btnVolverCargos_Click(object sender, EventArgs e)
        {
            panelCargos.Visible = false;
        }

        private void btnVolverPersonal_Click(object sender, EventArgs e)
        {
            panelRegistros.Visible = false;
        }

        private void btnGuardarCambiosC_Click(object sender, EventArgs e)
        {
            EditarCargos();
        }

        private void EditarCargos()
        {
            Lcargos parametros = new Lcargos();
            Dcargos funcion = new Dcargos();
            parametros.Id_cargo = IdCargo;
            parametros.Cargo = txtCargoG.Text;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoG.Text);
            if (funcion.editar_Cargo(parametros) == true)
            {
                txtCargo.Clear();
                BucarCargos();
                panelCargos.Visible = false;
            }
        }

        private void Personal_Load(object sender, EventArgs e)
        {

        }
    }
}
