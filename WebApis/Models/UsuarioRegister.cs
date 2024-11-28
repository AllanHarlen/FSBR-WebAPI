
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApis.Models
{
    public class UsuarioLogin
    {
        public string Login { get; set; }
        public string Senha { get; set; }

    }
}
