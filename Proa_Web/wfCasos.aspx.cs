using Proa_DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proa_Web
{
    public partial class wfCasos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarServicios();

                    CargarEspecs();

                    CargarDefaultQS();
                }
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Carga los valores de servicio y especialidad por default en caso de redirect desde otra página
        /// </summary>
        private void CargarDefaultQS()
        {
            if (Request.QueryString[clsGlobals.QS_IDSERV] != null)
            {
                ddlServicio.SelectedValue = Request.QueryString[clsGlobals.QS_IDSERV];
                CargarEspecs();
                ddlEspecialidad.SelectedValue = Request.QueryString[clsGlobals.QS_IDESPC];
                CargarCasos();
            }
        }

        /// <summary>
        /// Cargar las listas de Servicios
        /// </summary>
        private void CargarServicios()
        {
            Proa_Entities ent = new Proa_Entities();
            string CM = clsGlobals.CM();

            var servs = from s in ent.vw_SERVICIOS_POR_CENTRO
                        where s.COD_CENTRO_MEDICO.Equals(CM) && s.COD_SERVICIO_PADRE.Equals(null)
                        select s;

            ddlServicio.DataSource = servs.ToList().OrderBy(s => s.DESCRIPCION).ToList();
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

            ddlEspecialidad.DataSource = servs.ToList().OrderBy(s => s.DESCRIPCION).ToList();
            ddlEspecialidad.DataTextField = "DESCRIPCION";
            ddlEspecialidad.DataValueField = "COD_SERVICIO";
            ddlEspecialidad.DataBind();
        }

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarEspecs();

                CargarCasos();
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarCasos();
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Carga los casos reistrados de acuerdo al filtro
        /// </summary>
        private void CargarCasos()
        {
            Proa_Entities ent = new Proa_Entities();

            List<sp_lista_Estancias_x_Serv_Espec_Result> lCasos;

            lCasos = ent.sp_lista_Estancias_x_Serv_Espec(ddlServicio.SelectedValue, ddlEspecialidad.SelectedValue, clsGlobals.CM()).ToList();
            grdResultadoBusq.DataSource = lCasos;
            grdResultadoBusq.DataBind();
        }
    }
}