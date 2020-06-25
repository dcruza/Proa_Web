using Proa_DA;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proa_Web
{
    public partial class wfAbrirEstancia : System.Web.UI.Page
    {
        public string idPac
        {
            get
            {
                return hdfIpPac.Value;
            }
            set
            {
                hdfIpPac.Value = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ObtenerIdPac();

                    //pnlErrors.Visible = false;

                    txtFechaIngreso.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");

                    CargarServs();

                    CargarEspecs();

                    CargarDatosEstancias();
                }
                else
                {
                    OnConfirmMsgBox();
                }
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Invocar el método que carga la info del paciente
        /// </summary>
       // private void CargarInfoPac()
        //{
            //casoPaciente.CargarDatosEstancias();
        //}

        /// <summary>
        /// Obtiene el Id del paciente recibido por QS
        /// </summary>
        private void ObtenerIdPac()
        {
           idPac = Request.QueryString[clsGlobals.QS_IDPAC].ToString();            
        }

        /// <summary>
        /// Cargar las listas de Servicios
        /// </summary>
        private void CargarServs()
        {
            Proa_Entities ent = new Proa_Entities();
            string CM = clsGlobals.CM();

            var servs = from s in ent.vw_SERVICIOS_POR_CENTRO
                        orderby s.DESCRIPCION
                        where s.COD_CENTRO_MEDICO.Equals(CM) && s.COD_SERVICIO_PADRE.Equals(null)
                        select new { DESCRIPCION = s.DESCRIPCION.Trim(), COD_SERVICIO = s.COD_SERVICIO.Trim() };

            ddlServicio.DataSource = servs.ToList();
            ddlServicio.DataTextField = "DESCRIPCION";
            ddlServicio.DataValueField = "COD_SERVICIO";
            ddlServicio.DataBind();
        }

        /// <summary>
        /// Cargar las listas de Especialidades
        /// </summary>
        private void CargarEspecs()
        {
            Proa_Entities ent = new Proa_Entities();
            string CM = clsGlobals.CM();

            var servs = from s in ent.vw_SERVICIOS_POR_CENTRO
                        where s.COD_CENTRO_MEDICO.Equals(CM) && s.COD_SERVICIO_PADRE.Equals(ddlServicio.SelectedValue)
                        select s;

            ddlEspec.DataSource = servs.ToList().OrderBy(s => s.DESCRIPCION).ToList();
            ddlEspec.DataTextField = "DESCRIPCION";
            ddlEspec.DataValueField = "COD_SERVICIO";
            ddlEspec.DataBind();
        }

        /// <summary>
        /// Carga info de estancias abiertas del paciente
        /// </summary>
        public void CargarDatosEstancias()
        {
            Proa_Entities ent = new Proa_Entities();
            List<vw_Estancia> lvw = ent.vw_Estancia.Where(e => e.IDENTIFICACION == hdfIpPac.Value).ToList();
            List<sp_lista_Estancias_x_Id_Result> lep = ent.sp_lista_Estancias_x_Id(hdfIpPac.Value, "2103").ToList();

            foreach (vw_Estancia estancia in lvw)
            {
                // Si el caso no concuerda con otro caso con el mismo ID de paciente y número de caso
                if (lep.Where(l => l.idPaciente == estancia.IDENTIFICACION && l.numCaso == estancia.CASO).Count() == 0)
                {
                    lep.Add(new sp_lista_Estancias_x_Id_Result()
                    {
                        idPaciente = estancia.IDENTIFICACION,
                        numCaso = estancia.CASO,
                        NombrePac = estancia.APELLIDO1 + ' ' + estancia.APELLIDO2 + ' ' + estancia.NOMBRE,
                        fechaEgreso = estancia.FECHA_EGRESO,
                        fechaIngreso = estancia.FECHA_INGRESO,

                        codServicioInicial = estancia.COD_SERVICIO_ACTUAL_PADRE,
                        codEspecialidadInicial = estancia.COD_SERVICIO_ACTUAL,

                        ServicioActual = estancia.SERVICIO_ACTUAL_PADRE,
                        EspecialidadInicial = estancia.SERVICIO_ACTUAL,
                        Tipo = clsGlobals.CASO_ARCA_MEDISYS
                    });
                }
            }

            grdResultadoBusq.DataSource = lep;
            grdResultadoBusq.DataBind();
        }


        /// <summary>
        /// Se crea evento para ser capturado en formulario padre
        /// </summary>
        //[Browsable(true)]
        //[Category("Action")]
        //[Description("Se invoca cuando el usuario cambia el servicio seleccionado")]
        public event EventHandler ServicioCambiado;

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarEspecs();

                // Disparar o invocar evento personalizado para ser capturado en Formulario Padre
                if (ServicioCambiado != null)
                {
                    ServicioCambiado(this, EventArgs.Empty);
                }

            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                BuscarDx();

                // Disparar o invocar evento personalizado para ser capturado en Formulario Padre
                if (ServicioCambiado != null)
                    ServicioCambiado(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Realizar búsqueda de Dxs
        /// </summary>
        private void BuscarDx()
        {
            Proa_Entities ent = new Proa_Entities();

            List<vw_CIE104D> vw = ent.vw_CIE104D.Where(e => e.COD4D.Contains(txtCodDx.Text)
                                    && e.DESCRIPCION.Contains(txtDescDx.Text)).ToList();

            grdDx.DataSource = vw;
            grdDx.DataBind();
        }

        protected void grdDx_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdDx.PageIndex = e.NewPageIndex;
                BuscarDx();
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        protected void grdDx_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row;

            try
            {
                if (e.CommandName != "Page")
                {
                    row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    grdDx.SelectRow(row.RowIndex);
                }
                else
                {
                    grdDx.SelectRow(-1);
                }
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    GuardarEstancia((hdfCasoArca.Value == bool.TrueString ? true : false));

                    LimpiarTodo();

                    CargarDatosEstancias();

                    msgBox.ShowConfirm(Page.Title, "Los datos de la estancia se almacenaron correctamente. ¿Desea regresar a la lista de casos por Servicio?", clsGlobals.messageTypes.info, clsGlobals.CONFIRM_REGRESAR);                    
                }
                
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Evento que ocurre después de mostrar el confirmMessage
        /// </summary>
        public void OnConfirmMsgBox()
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == clsGlobals.CONFIRM_REGRESAR)
            {
                Response.Redirect("wfCasos.aspx?" + clsGlobals.QS_IDSERV + "=" + hdfIdServ.Value + "&" + clsGlobals.QS_IDESPC + "=" + hdfIdEspec.Value, false);            
            }
        }

        /// <summary>
        /// Guarda la estancia en sistema PROA
        /// </summary>
        /// <param name="esCasoArca">Indica si es un caso importado de ARCA o no</param>
        /// <param name="row">Row del datagrid en caso de que sean un caso ARCA</param>
        private void GuardarEstancia(bool esCasoArca)
        {
            Proa_Entities ent = new Proa_Entities();
            estanciaPaciente estPac = new estanciaPaciente();


            // Datos Generales
            estPac.idPaciente = hdfIpPac.Value;
            estPac.activo = true;
            estPac.codCentroMedico = clsGlobals.CM();
            estPac.fechaHoraIn = DateTime.Now;
            estPac.idUsuarioIn = clsGlobals.codUsuarioActivo;
            estPac.esCasoArca = esCasoArca;
            estPac.fechaEgreso = null;
            estPac.cod4DIngreso = grdDx.SelectedRow.Cells[0].Text;
            estPac.codEspecialidadInicial = ddlEspec.SelectedValue;
            estPac.codServicioInicial = ddlServicio.SelectedValue;
            estPac.fechaIngreso = DateTime.Parse(txtFechaIngreso.Text);
            estPac.numCaso = int.Parse(txtCaso.Text);

            // Conservar dtos en caso de que se deba redireccionar la página a la de casos registtrados.
            hdfIdServ.Value = estPac.codServicioInicial;
            hdfIdEspec.Value = estPac.codEspecialidadInicial;

            ent.estanciaPaciente.Add(estPac);
            ent.SaveChanges();
        }

        /// <summary>
        /// Valida campos obligatorios
        /// </summary>
        /// <returns></returns>
        private bool ValidarCampos()
        {
            DateTime dt;
            int nc;
            bool valido = true;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");

            if (!DateTime.TryParse(txtFechaIngreso.Text, out dt))
            {
                valido = false;
                sb.AppendLine(" * Fecha Ingreso");
            }
            if (!int.TryParse(txtCaso.Text, out nc))
            {
                valido = false;
                sb.AppendLine(" * N° Caso");
            }
            if (ddlServicio.SelectedItem == null)
            {
                valido = false;
                sb.AppendLine(" * Servicio");
            }
            
            if (ddlEspec.Text == string.Empty)
            {
                valido = false;
                sb.AppendLine(" * Especialidad");
            }
            if (grdDx.SelectedIndex < 0)
            {
                valido = false;
                sb.AppendLine(" * Diagnóstico Inicial");
            }

            if (!valido)
            {
                msgBox.ShowMsg(Page.Title, "Estos campos son obligatorios: " + sb.ToString(), clsGlobals.messageTypes.alert);
            }

            return valido;
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarTodo();

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "Close()", true);
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        protected void grdResultadoBusq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ProcesarFilasGrid(e);
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Procesar cada fila del grid para identificar su origen y determinar acciones
        /// </summary>
        private void ProcesarFilasGrid(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((sp_lista_Estancias_x_Id_Result)e.Row.DataItem).Tipo == clsGlobals.CASO_ARCA_MEDISYS)
                {
                    ((Label)e.Row.FindControl("lblOrigen")).Text = "ARCA";

                    ((ImageButton)e.Row.FindControl("ibtnAgregar")).Visible = true;

                    e.Row.Cells[2].BackColor = Color.LightGoldenrodYellow;
                    e.Row.Cells[2].ForeColor = Color.Blue;
                }
                else if (((sp_lista_Estancias_x_Id_Result)e.Row.DataItem).Tipo == clsGlobals.CASO_PROA)
                {
                    ((Label)e.Row.FindControl("lblOrigen")).Text = "PROA";

                    ((ImageButton)e.Row.FindControl("ibtnAgregar")).Visible = false;

                    e.Row.Cells[2].BackColor = Color.LightBlue;
                }
            }
        }

        protected void grdResultadoBusq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row;

            try
            {
                if (e.CommandName != "Page")
                {
                    row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    grdResultadoBusq.SelectRow(row.RowIndex);

                    // Agregar información
                    if (e.CommandName == clsGlobals.GRD_AGREGAR)
                    {
                        grdResultadoBusq.SelectRow(row.RowIndex);

                        CargarDatosArcaMed(row);
                    }
                }
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Carga los datos de la fila seleccionada en el grid y los coloca en los campos de edición
        /// </summary>
        /// <param name="row">Fila seleccionada del grid</param>
        private void CargarDatosArcaMed(GridViewRow row)
        {
            HiddenField hdfCodServ, hdfCodEspec;
            DateTime dt;

            hdfCodServ = (HiddenField)row.FindControl("hdfCodServ");
            hdfCodEspec = (HiddenField)row.FindControl("hdfCodEspec");

            dt = DateTime.Parse(row.Cells[7].Text);

            txtFechaIngreso.Text = dt.ToString("yyyy-MM-dd");

            txtCaso.Text = row.Cells[1].Text;
            ddlServicio.SelectedValue = hdfCodServ.Value;
            CargarEspecs();
            ddlEspec.SelectedValue = hdfCodEspec.Value;

            hdfCasoArca.Value = bool.TrueString;
        }

        /// <summary>
        /// Limpiar todos los controles
        /// </summary>
        private void LimpiarTodo()
        {
            hdfCasoArca.Value = bool.FalseString;
            txtFechaIngreso.Text = string.Empty;
            txtCaso.Text = string.Empty;
            ddlServicio.SelectedIndex = -1;
            ddlEspec.SelectedIndex = -1;
            txtCodDx.Text = string.Empty;
            txtDescDx.Text = string.Empty;
            grdDx.DataSource = null;
            grdDx.DataBind();
            grdResultadoBusq.SelectRow(-1);
        }
    }
}