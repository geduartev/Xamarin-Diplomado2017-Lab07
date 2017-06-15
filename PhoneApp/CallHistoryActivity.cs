using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    /// <summary>
    /// Esta clase crea una ListActivity, llenándola mediante código por lo que no se necesita un archivo .axml.
    /// </summary>
    [Activity(Label = "@string/CallHistory")]
    public class CallHistoryActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var PhoneNumbers = 
                Intent.Extras.GetStringArrayList("phone_numbers") ?? 
                new string[0];

            this.ListAdapter = new ArrayAdapter<string>(
                this, 
                Android.Resource.Layout.SimpleListItem1, 
                PhoneNumbers);
        }
    }
}