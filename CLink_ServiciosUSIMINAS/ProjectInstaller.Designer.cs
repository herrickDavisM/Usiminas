namespace CLink_ServiciosUSIMINAS
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.CLink_ServiceUSIMINAS = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServiceUSIMINAS = new System.ServiceProcess.ServiceInstaller();
            // 
            // CLink_ServiceUSIMINAS
            // 
            this.CLink_ServiceUSIMINAS.Password = null;
            this.CLink_ServiceUSIMINAS.Username = null;
            // 
            // ServiceUSIMINAS
            // 
            this.ServiceUSIMINAS.Description = "Servicio que envia los Resultados para USIMINAS";
            this.ServiceUSIMINAS.DisplayName = "CLink_ServiceUSIMINAS";
            this.ServiceUSIMINAS.ServiceName = "CLink_ServiceUSIMINAS";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.CLink_ServiceUSIMINAS,
            this.ServiceUSIMINAS});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller CLink_ServiceUSIMINAS;
        private System.ServiceProcess.ServiceInstaller ServiceUSIMINAS;
    }
}