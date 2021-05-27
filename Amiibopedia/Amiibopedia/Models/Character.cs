using System;
using System.Collections.Generic;
using System.Text;

namespace Amiibopedia.Models
{
    //usmos petición get api Character. Devuelve el nombre del amiibo con una llave única
    public class Character
    {
        public string key { get; set; }
        public string name { get; set; }
    }

    public class Characters
    {
        public List<Character> amiibo { get; set; }
    }
}
