using Proa_DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proa_Web
{
    public partial class wfPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    OnConfirmMsgBox();
                }
                else
                {
                    hdfBusquedaArcaMedisys.Value = clsGlobals.BUSCA_ARCA_MEDISYS.NO.ToString();

                    VisibilidadLlaves(true);
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
                LimpiarCamposEdicion();

                // Si no está marcado el check de búsqueda en otras bases de datos
                if (chkSoloBuscArcaMedisys.Checked == false)
                {
                    BuscarPaciente();
                }
                else // Búsqueda en BD externa
                {
                    hdfBusquedaArcaMedisys.Value = clsGlobals.BUSCA_ARCA_MEDISYS.SI.ToString();

                    BuscarPacienteBDExterno();
                }               
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Buscar la información del paciente según sus datos: ID, Apellido1, Apellido2, Nombre
        /// </summary>
        private void BuscarPaciente()
        {
            List<sp_BuscarPacienteProa_Result> lPacs;
            Proa_Entities ent;

            if (ValidarParametrosBusquedaPac())
            {
                ent = new Proa_Entities();

                lPacs = ent.sp_BuscarPacienteProa(txtIdPacBusq.Text.Trim(),
                                          txtNomPacBusq.Text.Trim(),
                                          txtApel1PacBusq.Text.Trim(),
                                          txtApel2PacBusq.Text.Trim()).ToList();

                if (lPacs.Count > 0)
                {
                    grdResultadoBusq.DataSource = lPacs.ToList();
                    grdResultadoBusq.DataBind();
                    Session[clsGlobals.SS_NOM_LISTAPACS] = lPacs;
                }
                else
                {
                    grdResultadoBusq.DataSource = null;
                    grdResultadoBusq.DataBind();
                    Session[clsGlobals.SS_NOM_LISTAPACS] = null;

                    msgBox.ShowConfirm(Page.Title, "El paciente no aparece en el registro de este sistema. ¿Desea buscar el paciente en los registros de los sistemas ARCA / Medisys?", clsGlobals.messageTypes.confirm, clsGlobals.CONFIRM_BUSQ_ARCA_MEDISYS);
                }

            }
            else
            {
                msgBox.ShowMsg(Page.Title, "Para realizar la búsqueda debe indicar el número de identificación o al menos 3 caracteres del nombre y apellidos", clsGlobals.messageTypes.alert);
            }
        }

        /// <summary>
        /// Evento que ocurre después de mostrar el confirmMessage
        /// </summary>
        public void OnConfirmMsgBox()
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == clsGlobals.CONFIRM_BUSQ_ARCA_MEDISYS)
            {
                BuscarPacienteBDExterno();

                hdfBusquedaArcaMedisys.Value = clsGlobals.BUSCA_ARCA_MEDISYS.SI.ToString();
            }
        }

        /// Buscar la información del paciente según sus datos: ID, Apellido1, Apellido2, Nombre
        /// en las tablas del sistema ARCA/MEDISYS
        private void BuscarPacienteBDExterno()
        {
            List<sp_BuscarPacienteARCAMEDISYS_Result> lPacs;
            Proa_Entities ent;

            if (ValidarParametrosBusquedaPac())
            {
                ent = new Proa_Entities();

                lPacs = ent.sp_BuscarPacienteARCAMEDISYS(txtIdPacBusq.Text.Trim(),
                                                         txtNomPacBusq.Text.Trim(),
                                                         txtApel1PacBusq.Text.Trim(),
                                                         txtApel2PacBusq.Text.Trim()).ToList();

                if (lPacs.Count > 0)
                {
                    grdResultadoBusq.DataSource = lPacs.ToList();
                    grdResultadoBusq.DataBind();
                    Session[clsGlobals.SS_NOM_LISTAPACS] = lPacs;
                }
                else
                {
                    msgBox.ShowMsg(Page.Title, "Lo sentimos, no se encontraron pacientes registrados con los datos de búsqueda digitados.", clsGlobals.messageTypes.alert);
                }

            }
            else
            {
                msgBox.ShowMsg(Page.Title, "Para realizar la búsqueda debe indicar el número de identificación o al menos 3 caracteres del nombre y apellidos", clsGlobals.messageTypes.alert);
            }
        }

        /// <summary>
        /// Valida que a la hora de buscar un paciente, se digite al menos 3
        /// caracteres de campos como nombre, apellidos o ID
        /// </summary>
        /// <returns></returns>
        private bool ValidarParametrosBusquedaPac()
        {
            if (txtIdPacBusq.Text.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                if ((txtNomPacBusq.Text.Trim().Length >= 3 && txtApel1PacBusq.Text.Trim().Length >= 3)
                   | (txtNomPacBusq.Text.Trim().Length >= 3 && txtApel2PacBusq.Text.Trim().Length >= 3)
                   | (txtApel1PacBusq.Text.Trim().Length >= 3 && txtApel2PacBusq.Text.Trim().Length >= 3))
                {
                    return true;
                }
                else
                {
                    return false;
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

                    // Editar información
                    if (e.CommandName == clsGlobals.GRD_EDIT)
                    {                       
                        CargarEdicionPac(row);
                    }
                }
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Bloquea o desbloquea los campos que se relacionan con
        /// llaves de la tabla de pacientes en la BD
        /// </summary>
        /// <param name="bloquear">Indica si se bloquea o no</param>
        public void VisibilidadLlaves(bool bloquear)
        {
            txtIdPac.Enabled = !bloquear;
        }

        /// <summary>
        /// Traslada los datos del paciente seleccionado en el grid de
        /// búsquedas a la sección de detalle de paciente
        /// </summary>
        /// <param name="row">Fila seleccionada en el grid</param>
        private void CargarEdicionPac(GridViewRow row)
        {
            Proa_Entities ent;
            List<paciente> lpacs;
            String idPac;
            List<sp_BuscarPacienteARCAMEDISYS_Result> lPacsArcaMedisys;

            ent = new Proa_Entities();
            idPac = row.Cells[1].Text;

            // Si se está buscando en la BD local de PROA
            if (hdfBusquedaArcaMedisys.Value == clsGlobals.BUSCA_ARCA_MEDISYS.NO.ToString())
            {
                lpacs = ent.paciente.Where(p => p.idPaciente == idPac).ToList();
            }
            else // Si se está buscando en ARCA / Medisys
            {
                lPacsArcaMedisys = (List<sp_BuscarPacienteARCAMEDISYS_Result>)Session[clsGlobals.SS_NOM_LISTAPACS];
                lpacs = ConvertirPacienteArcaMedisys(lPacsArcaMedisys.Where(l => l.idPaciente == idPac).ToList());
            }

            if (lpacs.Count == 1)
            {
                txtIdPac.Text = lpacs.First().idPaciente;
                txtNomPac.Text = lpacs.First().nombre;
                txtApel1Pac.Text = lpacs.First().apellido1;
                txtApel2Pac.Text = lpacs.First().apellido2;
                txtFechaNacim.Text = lpacs.First().fechaNacim.ToShortDateString();
                rblSexo.SelectedValue = lpacs.First().sexo;

                txtIdMadre.Text = lpacs.First().idMadre;
                txtNomMadre.Text = lpacs.First().nombreMadre;
                txtApel1Madre.Text = lpacs.First().apellido1Madre;
                txtApel2Madre.Text = lpacs.First().apellido2Madre;

                txtIdPadre.Text = lpacs.First().idPadre;
                txtNomPadre.Text = lpacs.First().nombrePadre;
                txtApel1Padre.Text = lpacs.First().apellido1Padre;
                txtApel2Padre.Text = lpacs.First().apellido2Padre;

                CargarTelefonosPac(lpacs.First());
            }
        }

        private List<paciente> ConvertirPacienteArcaMedisys(List<sp_BuscarPacienteARCAMEDISYS_Result> resultsArcaMedisys)
        {
            Proa_Entities ent = new Proa_Entities();
            List<paciente> lPacs = new List<paciente>();
            paciente pac;
            telefonoPaciente telef;
            List<telefonoPaciente> lTelefs = new List<telefonoPaciente>();

            foreach (sp_BuscarPacienteARCAMEDISYS_Result item in resultsArcaMedisys)
            {
                pac = new paciente();

                pac.activo = item.activo.Value;
                pac.apellido1 = item.apellido1;
                pac.apellido1Madre = item.apellido1Madre;
                pac.apellido1Padre = item.apellido1Padre;
                pac.apellido2 = item.apellido2;
                pac.apellido2Madre = item.apellido2Madre;
                pac.apellido2Padre = item.apellido2Padre;
                pac.fechaHoraIn = DateTime.Now;
                pac.fechaNacim = item.fechaNacim.Value;
                pac.idMadre = item.idMadre.ToString();
                pac.idPaciente = item.idPaciente;
                pac.idPadre = item.idPadre.ToString();
                pac.idUsuarioIn = clsGlobals.codUsuarioActivo;
                pac.nombre = item.nombre;
                pac.nombreMadre = item.nombreMadre;
                pac.nombrePadre = item.nombrePadre;
                pac.sexo = item.sexo;

                { // Convertir los teléfonos

                    telef = new telefonoPaciente();
                    telef.activo = true;
                    telef.fechaHoraIn = DateTime.Now;
                    telef.idPaciente = pac.idPaciente;
                    telef.tipoTelefono = ent.tipoTelefono.Where(t => t.esCasa).First();
                    telef.idTipoTelefono = telef.tipoTelefono.id;
                    telef.numero = item.TelefCasa;
                    telef.idUsuarioIn = clsGlobals.codUsuarioActivo;
                    lTelefs.Add(telef);

                    telef = new telefonoPaciente();
                    telef.activo = true;
                    telef.fechaHoraIn = DateTime.Now;
                    telef.idPaciente = pac.idPaciente;
                    telef.tipoTelefono = ent.tipoTelefono.Where(t => t.esCelular).First();
                    telef.idTipoTelefono = telef.tipoTelefono.id;
                    telef.numero = item.TelefCel;
                    telef.idUsuarioIn = clsGlobals.codUsuarioActivo;
                    lTelefs.Add(telef);

                    telef = new telefonoPaciente();
                    telef.activo = true;
                    telef.fechaHoraIn = DateTime.Now;
                    telef.idPaciente = pac.idPaciente;
                    telef.tipoTelefono = ent.tipoTelefono.Where(t => t.esTrabajo).First();
                    telef.idTipoTelefono = telef.tipoTelefono.id;
                    telef.numero = item.TelefOfi;
                    telef.idUsuarioIn = clsGlobals.codUsuarioActivo;
                    lTelefs.Add(telef);

                    telef = new telefonoPaciente();
                    telef.activo = true;
                    telef.fechaHoraIn = DateTime.Now;
                    telef.idPaciente = pac.idPaciente;
                    telef.tipoTelefono = ent.tipoTelefono.Where(t => t.esUrgencia).First();
                    telef.idTipoTelefono = telef.tipoTelefono.id;
                    telef.numero = item.TelefUrg;
                    telef.idUsuarioIn = clsGlobals.codUsuarioActivo;
                    lTelefs.Add(telef);

                    pac.telefonoPaciente = (ICollection<telefonoPaciente>)lTelefs;
                }

                lPacs.Add(pac);
            }

            return lPacs;
        }

        /// <summary>
        /// Desglosa los teléfonos del paciente en sus respectivos 
        /// campos del formulario
        /// </summary>
        /// <param name="paciente"></param>
        private void CargarTelefonosPac(paciente paciente)
        {
            List<telefonoPaciente> tels = paciente.telefonoPaciente.ToList();

            foreach (telefonoPaciente tel in tels)
            {
                if (tel.tipoTelefono.esCasa)
                {
                    txtTelCasa.Text = tel.numero;
                }
                if (tel.tipoTelefono.esCelular)
                {
                    txtTelCel.Text = tel.numero;
                }
                if (tel.tipoTelefono.esTrabajo)
                {
                    txtTelOfi.Text = tel.numero;
                }
                if (tel.tipoTelefono.esUrgencia)
                {
                    txtTelUrg.Text = tel.numero;
                }
            }
        }

        /// <summary>
        /// Limpia todos los campos de edición
        /// </summary>
        private void LimpiarCamposEdicion()
        {
            txtIdPac.Text = string.Empty;
            txtNomPac.Text = string.Empty;
            txtApel1Pac.Text = string.Empty;
            txtApel2Pac.Text = string.Empty;
            txtFechaNacim.Text = string.Empty;
            rblSexo.SelectedIndex = -1;

            txtIdMadre.Text = string.Empty;
            txtNomMadre.Text = string.Empty;
            txtApel1Madre.Text = string.Empty;
            txtApel2Madre.Text = string.Empty;

            txtIdPadre.Text = string.Empty;
            txtNomPadre.Text = string.Empty;
            txtApel1Padre.Text = string.Empty;
            txtApel2Padre.Text = string.Empty;

            txtTelCasa.Text = string.Empty;
            txtTelCel.Text = string.Empty;
            txtTelOfi.Text = string.Empty;
            txtTelUrg.Text = string.Empty;

            grdResultadoBusq.SelectRow(-1);
            Session[clsGlobals.SS_NOM_LISTAPACS] = null;

            hdfBusquedaArcaMedisys.Value = clsGlobals.BUSCA_ARCA_MEDISYS.NO.ToString();

            //BloquearLlaves(false);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposEdicion();
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        protected void grdResultadoBusq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResultadoBusq.PageIndex = e.NewPageIndex;
                grdResultadoBusq.DataSource = Session[clsGlobals.SS_NOM_LISTAPACS];
                grdResultadoBusq.DataBind();
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Validar datos requeridos antes de guardar la información en BD
        /// </summary>
        /// <returns></returns>
        private bool ValidarData()
        {
            DateTime dtresult;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");

            if (txtIdPac.Text.Trim() == string.Empty)
            {
                sb.AppendLine(" * Id del Paciente");
            }
            if (txtNomPac.Text.Trim() == string.Empty)
            {
                sb.AppendLine(" * Nombre del Paciente");
            }
            if (txtApel1Pac.Text.Trim() == string.Empty)
            {
                sb.AppendLine(" * Apellido 1 del Paciente");
            }
            if (txtApel2Pac.Text.Trim() == string.Empty)
            {
                sb.AppendLine(" * Apellido 2 del Paciente");
            }
            if (!DateTime.TryParse(txtFechaNacim.Text, out dtresult))
            {
                sb.AppendLine(" * Fecha de Nacimiento");
            }
            if (rblSexo.SelectedIndex < 0)
            {
                sb.AppendLine(" * Sexo");
            }

            // Verificar si es un paciente manual
            if(hdfPacManual.Value == clsGlobals.PACIENTE_MANUAL.NO.ToString())
            {
                if(grdResultadoBusq.SelectedIndex < 0)
                {
                    sb.AppendLine(" * Seleccione un paciente de la Lista");
                }
            }

            if (sb.Length > 2)
            {
                msgBox.ShowMsg(Page.Title, "Estos campos son obligatorios: " + sb.ToString(), clsGlobals.messageTypes.alert);

                return false;
            }
            else
                return true;
        }

        protected void btnGuardarPac_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarData())
                {
                    GuardarInfoPaciente(hdfPacManual.Value == clsGlobals.PACIENTE_MANUAL.SI.ToString());

                    LimpiarCamposEdicion();

                    msgBox.ShowMsg(Page.Title, "¡La información del Paciente se almacenó de manera correcta! ", clsGlobals.messageTypes.info);
                }
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Guarda la información del paciente en la BD de PROA
        /// </summary>
        /// <param name="esPacienteManual">Indica si el paciente se está creando manualmente, es decir,
        /// no en paciente registrado en la BD de Proa ni en ARCA / Medisys</param>
        private void GuardarInfoPaciente(bool esPacienteManual)
        {
            paciente pac;
            Proa_Entities ent = new Proa_Entities();           
            string idPac = txtIdPac.Text.Trim();
            bool esNuevoEnProa;

            // Se trata de una búsqueda de pacientes en PROA, o si se trata de una
            // búsqueda de pacientes en ARCA / Medisys pero el paciente ya existe en PROA
            if (!esPacienteManual && ((hdfBusquedaArcaMedisys.Value != clsGlobals.BUSCA_ARCA_MEDISYS.SI.ToString())
                || (hdfBusquedaArcaMedisys.Value == clsGlobals.BUSCA_ARCA_MEDISYS.SI.ToString()
                   & ent.paciente.Where(p => p.idPaciente == idPac).ToList().Count > 0)))
            {
                pac = ent.paciente.Where(p => p.idPaciente == txtIdPac.Text.Trim()).First();

                esNuevoEnProa = false;
            }
            else
            // Se trata de un paciente que se buscó de ARCA / Medisys, es decir, un paciente nuevo
            // para el sistema PROA
            {
                pac = new paciente();

                esNuevoEnProa = true;
            }            

            // Introducir datos
            pac.activo = true;
            pac.apellido1 = txtApel1Pac.Text.Trim();
            pac.apellido1Madre = txtApel1Madre.Text.Trim();
            pac.apellido1Padre = txtApel1Padre.Text.Trim();
            pac.apellido2 = txtApel2Pac.Text.Trim();
            pac.apellido2Madre = txtApel2Madre.Text.Trim();
            pac.apellido2Padre = txtApel2Padre.Text.Trim();
            pac.fechaHoraIn = DateTime.Now;
            pac.fechaNacim = DateTime.Parse(txtFechaNacim.Text);
            pac.idMadre = txtIdMadre.Text.Trim();
            pac.idPaciente = idPac;
            pac.idPadre = txtIdPadre.Text.Trim();
            pac.idUsuarioIn = clsGlobals.codUsuarioActivo;
            pac.nombre = txtNomPac.Text.Trim();
            pac.nombreMadre = txtNomMadre.Text.Trim();
            pac.nombrePadre = txtNomPadre.Text.Trim();
            pac.sexo = rblSexo.SelectedValue;

            ent.telefonoPaciente.RemoveRange(pac.telefonoPaciente);
            pac.telefonoPaciente = EstablecerTelefonos(ref pac);

            // Si es un paciente nuevo en PROA
            if (esNuevoEnProa)
            {
                ent.paciente.Add(pac);
            }

            ent.SaveChanges();
        }

        /// <summary>
        /// Establece la lista de teléfonos del paciente de acuerdo a su tipo
        /// </summary>
        /// <param name="pac">Paciente al que se le asignan los telfs</param>
        /// <returns>Lista de Teléfonos asignados</returns>
        private List<telefonoPaciente> EstablecerTelefonos(ref paciente pac)
        {
            List<telefonoPaciente> tels;
            List<telefonoPaciente> telsBusq;
            telefonoPaciente tlf;

            if(pac.telefonoPaciente != null)
            {
                tels = pac.telefonoPaciente.ToList();
            }
            else
            {
                tels = new List<telefonoPaciente>();
            }

            // Teléfono casa
            telsBusq = tels.Where(t => t.idTipoTelefono == (short)clsGlobals.TIPO_TELEFS.CASA).ToList();
            if (txtTelCasa.Text.Trim().Length > 0)
            {                                
                if (telsBusq.Count > 0)
                {
                    tlf = telsBusq.First();
                    tlf.numero = txtTelCasa.Text.Trim();
                }
                else
                {
                    tels.Add(new telefonoPaciente()
                    {
                        activo = true,
                        fechaHoraIn = DateTime.Now,
                        idPaciente = pac.idPaciente,
                        idTipoTelefono = (short)clsGlobals.TIPO_TELEFS.CASA,
                        idUsuarioIn = clsGlobals.codUsuarioActivo,
                        numero = txtTelCasa.Text.Trim()
                    });
                }
            }
            else if(telsBusq.Count > 0)
            {
                // Eliminar el teléfono
                tels.Remove(telsBusq.First());
            }


            // Teléfono Trabajo
            if (txtTelOfi.Text.Trim().Length > 0)
            {
                telsBusq = tels.Where(t => t.idTipoTelefono == (short)clsGlobals.TIPO_TELEFS.TRABAJO).ToList();

                if (telsBusq.Count > 0)
                {
                    tlf = telsBusq.First();
                    tlf.numero = txtTelOfi.Text.Trim();
                }
                else
                {
                    tels.Add(new telefonoPaciente()
                    {
                        activo = true,
                        fechaHoraIn = DateTime.Now,
                        idPaciente = pac.idPaciente,
                        idTipoTelefono = (short)clsGlobals.TIPO_TELEFS.TRABAJO,
                        idUsuarioIn = clsGlobals.codUsuarioActivo,
                        numero = txtTelOfi.Text.Trim()
                    });
                }
            }

            // Teléfono Celular
            if (txtTelCel.Text.Trim().Length > 0)
            {
                telsBusq = tels.Where(t => t.idTipoTelefono == (short)clsGlobals.TIPO_TELEFS.CELULAR).ToList();

                if (telsBusq.Count > 0)
                {
                    tlf = telsBusq.First();
                    tlf.numero = txtTelCel.Text.Trim();
                }
                else
                {
                    tels.Add(new telefonoPaciente()
                    {
                        activo = true,
                        fechaHoraIn = DateTime.Now,
                        idPaciente = pac.idPaciente,
                        idTipoTelefono = (short)clsGlobals.TIPO_TELEFS.CELULAR,
                        idUsuarioIn = clsGlobals.codUsuarioActivo,
                        numero = txtTelCel.Text.Trim()
                    });
                }
            }

            // Teléfono Urgencia
            if (txtTelUrg.Text.Trim().Length > 0)
            {
                telsBusq = tels.Where(t => t.idTipoTelefono == (short)clsGlobals.TIPO_TELEFS.URGENCIA).ToList();

                if (telsBusq.Count > 0)
                {
                    tlf = telsBusq.First();
                    tlf.numero = txtTelUrg.Text.Trim();
                }
                else
                {
                    tels.Add(new telefonoPaciente()
                    {
                        activo = true,
                        fechaHoraIn = DateTime.Now,
                        idPaciente = pac.idPaciente,
                        idTipoTelefono = (short)clsGlobals.TIPO_TELEFS.URGENCIA,
                        idUsuarioIn = clsGlobals.codUsuarioActivo,
                        numero = txtTelUrg.Text.Trim()
                    });
                }
            }

            return tels;
        }

        protected void btnNuevaBusq_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCamposBusqueda();

                LimpiarCamposEdicion();
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Limpia los campos de búsqueda de pacientes
        /// </summary>
        private void LimpiarCamposBusqueda()
        {
            txtIdPacBusq.Text = string.Empty;
            txtNomPacBusq.Text = string.Empty;
            txtApel1PacBusq.Text = string.Empty;
            txtApel2PacBusq.Text = string.Empty;

            grdResultadoBusq.DataSource = null;
            grdResultadoBusq.DataBind();
        }

        protected void rdoBuscarPacs_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                hdfPacManual.Value = (rdoBuscarPacs.Checked ? clsGlobals.PACIENTE_MANUAL.NO.ToString() : clsGlobals.PACIENTE_MANUAL.SI.ToString());

                VisibilidadPanelesBusqueda(hdfPacManual.Value == clsGlobals.PACIENTE_MANUAL.NO.ToString());

                VisibilidadLlaves(hdfPacManual.Value == clsGlobals.PACIENTE_MANUAL.NO.ToString());

                LimpiarCamposBusqueda();

                LimpiarCamposEdicion();
            }
            catch (Exception ex)
            {
                msgBox.ShowMsg(Page.Title, clsGlobals.ERR_MESAGE + ex.Message, clsGlobals.messageTypes.error);
            }
        }

        /// <summary>
        /// Muestra u oculta los páneles de búsqueda de pacientes
        /// </summary>
        /// <param name="ocultar">Indica si se muestran u ocultan los páneles</param>
        private void VisibilidadPanelesBusqueda(bool ocultar)
        {
            pnlBusq1.Visible = ocultar;
            pnlBusq2.Visible = ocultar;

            //if (ocultar == true)
            //{
            //    VisibilidadLlaves(ocultar);
            //}
        }
    }

}
