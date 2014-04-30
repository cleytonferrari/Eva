using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Dominio
{
    public class Entidade
    {

        private string id;

        public string Id
        {
            get
            {
                if (!string.IsNullOrEmpty(id))
                {
                    return id;
                }
                id = Guid.NewGuid().ToString("N");
                return id;
            }
            set { id = value; }
        }
    }
}
