﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Klient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IZadanie1")]
    public interface IZadanie1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IZadanie1/DlugieObliczenia", ReplyAction="http://tempuri.org/IZadanie1/DlugieObliczeniaResponse")]
        string DlugieObliczenia();
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IZadanie1/DlugieObliczenia", ReplyAction="http://tempuri.org/IZadanie1/DlugieObliczeniaResponse")]
        System.IAsyncResult BeginDlugieObliczenia(System.AsyncCallback callback, object asyncState);
        
        string EndDlugieObliczenia(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IZadanie1/Szybciej", ReplyAction="http://tempuri.org/IZadanie1/SzybciejResponse")]
        string Szybciej(int x, int fx);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IZadanie1/Szybciej", ReplyAction="http://tempuri.org/IZadanie1/SzybciejResponse")]
        System.IAsyncResult BeginSzybciej(int x, int fx, System.AsyncCallback callback, object asyncState);
        
        string EndSzybciej(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IZadanie1Channel : Klient.ServiceReference1.IZadanie1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DlugieObliczeniaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public DlugieObliczeniaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SzybciejCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public SzybciejCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Zadanie1Client : System.ServiceModel.ClientBase<Klient.ServiceReference1.IZadanie1>, Klient.ServiceReference1.IZadanie1 {
        
        private BeginOperationDelegate onBeginDlugieObliczeniaDelegate;
        
        private EndOperationDelegate onEndDlugieObliczeniaDelegate;
        
        private System.Threading.SendOrPostCallback onDlugieObliczeniaCompletedDelegate;
        
        private BeginOperationDelegate onBeginSzybciejDelegate;
        
        private EndOperationDelegate onEndSzybciejDelegate;
        
        private System.Threading.SendOrPostCallback onSzybciejCompletedDelegate;
        
        public Zadanie1Client() {
        }
        
        public Zadanie1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Zadanie1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<DlugieObliczeniaCompletedEventArgs> DlugieObliczeniaCompleted;
        
        public event System.EventHandler<SzybciejCompletedEventArgs> SzybciejCompleted;
        
        public string DlugieObliczenia() {
            return base.Channel.DlugieObliczenia();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginDlugieObliczenia(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDlugieObliczenia(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndDlugieObliczenia(System.IAsyncResult result) {
            return base.Channel.EndDlugieObliczenia(result);
        }
        
        private System.IAsyncResult OnBeginDlugieObliczenia(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return this.BeginDlugieObliczenia(callback, asyncState);
        }
        
        private object[] OnEndDlugieObliczenia(System.IAsyncResult result) {
            string retVal = this.EndDlugieObliczenia(result);
            return new object[] {
                    retVal};
        }
        
        private void OnDlugieObliczeniaCompleted(object state) {
            if ((this.DlugieObliczeniaCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.DlugieObliczeniaCompleted(this, new DlugieObliczeniaCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void DlugieObliczeniaAsync() {
            this.DlugieObliczeniaAsync(null);
        }
        
        public void DlugieObliczeniaAsync(object userState) {
            if ((this.onBeginDlugieObliczeniaDelegate == null)) {
                this.onBeginDlugieObliczeniaDelegate = new BeginOperationDelegate(this.OnBeginDlugieObliczenia);
            }
            if ((this.onEndDlugieObliczeniaDelegate == null)) {
                this.onEndDlugieObliczeniaDelegate = new EndOperationDelegate(this.OnEndDlugieObliczenia);
            }
            if ((this.onDlugieObliczeniaCompletedDelegate == null)) {
                this.onDlugieObliczeniaCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnDlugieObliczeniaCompleted);
            }
            base.InvokeAsync(this.onBeginDlugieObliczeniaDelegate, null, this.onEndDlugieObliczeniaDelegate, this.onDlugieObliczeniaCompletedDelegate, userState);
        }
        
        public string Szybciej(int x, int fx) {
            return base.Channel.Szybciej(x, fx);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginSzybciej(int x, int fx, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSzybciej(x, fx, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndSzybciej(System.IAsyncResult result) {
            return base.Channel.EndSzybciej(result);
        }
        
        private System.IAsyncResult OnBeginSzybciej(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int x = ((int)(inValues[0]));
            int fx = ((int)(inValues[1]));
            return this.BeginSzybciej(x, fx, callback, asyncState);
        }
        
        private object[] OnEndSzybciej(System.IAsyncResult result) {
            string retVal = this.EndSzybciej(result);
            return new object[] {
                    retVal};
        }
        
        private void OnSzybciejCompleted(object state) {
            if ((this.SzybciejCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SzybciejCompleted(this, new SzybciejCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SzybciejAsync(int x, int fx) {
            this.SzybciejAsync(x, fx, null);
        }
        
        public void SzybciejAsync(int x, int fx, object userState) {
            if ((this.onBeginSzybciejDelegate == null)) {
                this.onBeginSzybciejDelegate = new BeginOperationDelegate(this.OnBeginSzybciej);
            }
            if ((this.onEndSzybciejDelegate == null)) {
                this.onEndSzybciejDelegate = new EndOperationDelegate(this.OnEndSzybciej);
            }
            if ((this.onSzybciejCompletedDelegate == null)) {
                this.onSzybciejCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSzybciejCompleted);
            }
            base.InvokeAsync(this.onBeginSzybciejDelegate, new object[] {
                        x,
                        fx}, this.onEndSzybciejDelegate, this.onSzybciejCompletedDelegate, userState);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IZadanie2", CallbackContract=typeof(Klient.ServiceReference1.IZadanie2Callback))]
    public interface IZadanie2 {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IZadanie2/PodajZadania")]
        void PodajZadania();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="http://tempuri.org/IZadanie2/PodajZadania")]
        System.IAsyncResult BeginPodajZadania(System.AsyncCallback callback, object asyncState);
        
        void EndPodajZadania(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IZadanie2Callback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IZadanie2/Zadanie")]
        void Zadanie([System.ServiceModel.MessageParameterAttribute(Name="zadanie")] string zadanie1, int pkt, bool zaliczone);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, AsyncPattern=true, Action="http://tempuri.org/IZadanie2/Zadanie")]
        System.IAsyncResult BeginZadanie(string zadanie, int pkt, bool zaliczone, System.AsyncCallback callback, object asyncState);
        
        void EndZadanie(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IZadanie2Channel : Klient.ServiceReference1.IZadanie2, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Zadanie2Client : System.ServiceModel.DuplexClientBase<Klient.ServiceReference1.IZadanie2>, Klient.ServiceReference1.IZadanie2 {
        
        private BeginOperationDelegate onBeginPodajZadaniaDelegate;
        
        private EndOperationDelegate onEndPodajZadaniaDelegate;
        
        private System.Threading.SendOrPostCallback onPodajZadaniaCompletedDelegate;
        
        public Zadanie2Client(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public Zadanie2Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public Zadanie2Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie2Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie2Client(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> PodajZadaniaCompleted;
        
        public void PodajZadania() {
            base.Channel.PodajZadania();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginPodajZadania(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginPodajZadania(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndPodajZadania(System.IAsyncResult result) {
            base.Channel.EndPodajZadania(result);
        }
        
        private System.IAsyncResult OnBeginPodajZadania(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return this.BeginPodajZadania(callback, asyncState);
        }
        
        private object[] OnEndPodajZadania(System.IAsyncResult result) {
            this.EndPodajZadania(result);
            return null;
        }
        
        private void OnPodajZadaniaCompleted(object state) {
            if ((this.PodajZadaniaCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.PodajZadaniaCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void PodajZadaniaAsync() {
            this.PodajZadaniaAsync(null);
        }
        
        public void PodajZadaniaAsync(object userState) {
            if ((this.onBeginPodajZadaniaDelegate == null)) {
                this.onBeginPodajZadaniaDelegate = new BeginOperationDelegate(this.OnBeginPodajZadania);
            }
            if ((this.onEndPodajZadaniaDelegate == null)) {
                this.onEndPodajZadaniaDelegate = new EndOperationDelegate(this.OnEndPodajZadania);
            }
            if ((this.onPodajZadaniaCompletedDelegate == null)) {
                this.onPodajZadaniaCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnPodajZadaniaCompleted);
            }
            base.InvokeAsync(this.onBeginPodajZadaniaDelegate, null, this.onEndPodajZadaniaDelegate, this.onPodajZadaniaCompletedDelegate, userState);
        }
    }
}
