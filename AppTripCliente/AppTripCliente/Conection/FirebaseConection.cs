using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTripCliente.Conection
{
    public class FirebaseConection
    {
        public static FirebaseClient firebase = new FirebaseClient("https://mvvmguia-ecc82-default-rtdb.firebaseio.com/");
    }
}
