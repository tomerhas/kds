﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
// 
#pragma warning disable 1591

namespace KdsLibrary.Barcode {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Configuration;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsBarCodeSoap", Namespace="http://tempuri.org/")]
    public partial class wsBarCode : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GenerateBarCodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback GenerateBarCodeNoTagOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsBarCode() {
            this.Url = ConfigurationSettings.AppSettings["WsBarcode"].ToString();
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GenerateBarCodeCompletedEventHandler GenerateBarCodeCompleted;
        
        /// <remarks/>
        public event GenerateBarCodeNoTagCompletedEventHandler GenerateBarCodeNoTagCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GenerateBarCode", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GenerateBarCode(string Text, int height, int width) {
            object[] results = this.Invoke("GenerateBarCode", new object[] {
                        Text,
                        height,
                        width});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GenerateBarCodeAsync(string Text, int height, int width) {
            this.GenerateBarCodeAsync(Text, height, width, null);
        }
        
        /// <remarks/>
        public void GenerateBarCodeAsync(string Text, int height, int width, object userState) {
            if ((this.GenerateBarCodeOperationCompleted == null)) {
                this.GenerateBarCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerateBarCodeOperationCompleted);
            }
            this.InvokeAsync("GenerateBarCode", new object[] {
                        Text,
                        height,
                        width}, this.GenerateBarCodeOperationCompleted, userState);
        }
        
        private void OnGenerateBarCodeOperationCompleted(object arg) {
            if ((this.GenerateBarCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerateBarCodeCompleted(this, new GenerateBarCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GenerateBarCodeNoTag", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GenerateBarCodeNoTag(string Text, int height, int width) {
            object[] results = this.Invoke("GenerateBarCodeNoTag", new object[] {
                        Text,
                        height,
                        width});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GenerateBarCodeNoTagAsync(string Text, int height, int width) {
            this.GenerateBarCodeNoTagAsync(Text, height, width, null);
        }
        
        /// <remarks/>
        public void GenerateBarCodeNoTagAsync(string Text, int height, int width, object userState) {
            if ((this.GenerateBarCodeNoTagOperationCompleted == null)) {
                this.GenerateBarCodeNoTagOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerateBarCodeNoTagOperationCompleted);
            }
            this.InvokeAsync("GenerateBarCodeNoTag", new object[] {
                        Text,
                        height,
                        width}, this.GenerateBarCodeNoTagOperationCompleted, userState);
        }
        
        private void OnGenerateBarCodeNoTagOperationCompleted(object arg) {
            if ((this.GenerateBarCodeNoTagCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerateBarCodeNoTagCompleted(this, new GenerateBarCodeNoTagCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GenerateBarCodeCompletedEventHandler(object sender, GenerateBarCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerateBarCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GenerateBarCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GenerateBarCodeNoTagCompletedEventHandler(object sender, GenerateBarCodeNoTagCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerateBarCodeNoTagCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GenerateBarCodeNoTagCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591