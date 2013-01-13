using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Managers {
    public class PerRequestLifetimeManager : LifetimeManager  {

        private const string _key = "SingletonPerRequest";

        public static string Key {
            get {
                return _key;
            }
        }

        public override object GetValue() {
            return HttpContext.Current.Items[_key];
        }

        public override void SetValue(object newValue) {
            HttpContext.Current.Items[_key] = newValue;
        }

        public override void RemoveValue() {
            if (HttpContext.Current.Items[_key] != null) {
                HttpContext.Current.Items.Remove(_key);
            }
        }
        
    }
}