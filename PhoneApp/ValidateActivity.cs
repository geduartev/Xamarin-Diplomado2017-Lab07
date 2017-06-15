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
    [Activity(Label = "@string/ValidateActivity", MainLauncher = false, Icon = "@drawable/icon")]
    public class ValidateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Validate);

            var ValidateButton = FindViewById<Button>(Resource.Id.ValidateButton);

            ValidateButton.Click += (object sender, System.EventArgs e) =>
            {
                Validate();
            };
        }

        private async void Validate()
        {
            var Email = FindViewById<EditText>(Resource.Id.EmailEditText);
            var Password = FindViewById<EditText>(Resource.Id.PasswordEditText);

            string myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            var ServiceClient = new SALLab07.ServiceClient();
            var Result = await ServiceClient.ValidateAsync(Email.Text, Password.Text, myDevice);

            if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var notification = new Notification.Builder(this)
                    .SetContentTitle("Validación de actividad")
                    .SetContentText($"{Result.Status}\n{Result.Fullname}\n{Result.Token}")
                    .SetSmallIcon(Resource.Drawable.Icon);

                notification.SetCategory(Notification.CategoryMessage);

                var ObjectNotification = notification.Build();

                var Manager = GetSystemService(
                    Android.Content.Context.NotificationService) as NotificationManager;

                Manager.Notify(0, ObjectNotification);
            }
            else
            {
                var ValidateMessage = FindViewById<TextView>(Resource.Id.ValidateMessageTextView);
                ValidateMessage.SetPadding(40, 20, 0, 0);
                ValidateMessage.Text = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
            }

        }
    }
}