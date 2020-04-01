//using RHListaElegibles.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proa_Web.UserControl
{
    public partial class mensajes : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Muestra los mensajes de la aplicación hacia el usuario
        /// </summary>
        /// <param name="titulo">Título del mensaje</param>
        /// <param name="texto">Cuerpo del mensaje</param>
        /// <param name="tipoMensaje">Tipo del mensaje (info, alert, error, confirm)</param>
        public void ShowMsg(string titulo, string texto, clsGlobals.messageTypes tipoMensaje)
        {            
            ClientScriptManager script = Page.ClientScript;

            script.RegisterStartupScript(GetType(), "Alert", "<script type=text/javascript>msgBox('" + titulo.Replace("\r\n","") + "', '" + texto.Replace("\r\n", "").Replace("'","") + "','" + tipoMensaje + "')</script>");          
        }

        /// <summary>
        /// Muestra los mensaje de confirmación y desencadena acciones para ejecutar una acción
        /// en una respuesta afirmativa
        /// </summary>
        /// <param name="titulo">Título del mensaje</param>
        /// <param name="texto">Cuerpo del mensaje</param>
        /// <param name="tipoMensaje">Tipo del mensaje (info, alert, error, confirm)</param>
        /// <param name="clave">Palabra clave para distinguir entre diferentes solicitudes de confirmación</param>
        public void ShowConfirm(string titulo, string texto, clsGlobals.messageTypes tipoMensaje, string clave)
        {
            ClientScriptManager script = Page.ClientScript;

            script.RegisterStartupScript(GetType(), "Confirm", "<script type=text/javascript>confirmBox('" + titulo.Replace("\r\n", "") + "', '" + texto.Replace("\r\n", "").Replace("'", "") + "','" + clave + "')</script>");
        }
    }
}