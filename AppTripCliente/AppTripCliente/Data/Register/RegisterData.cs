using AppTripCliente.Conection;
using AppTripCliente.Model;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTripCliente.Data.Register
{
    public class RegisterData
    {
        public async Task InsertUser(User user)
        {
            await FirebaseConection.firebase.Child("Users").Child(user.IdUser).PutAsync(user);
        }
    }
}
