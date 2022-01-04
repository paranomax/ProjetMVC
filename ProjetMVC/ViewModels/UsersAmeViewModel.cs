using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetMVC.Models;

namespace ProjetMVC.ViewModels
{
    public class UsersAmeViewModel
    {
        public User user { get; set; }
        
        public List<Weapon> weapon { get; set; }
    }
}