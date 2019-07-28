using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAS_Sustainability
{
    public class MainModel
    {
        public UserLogin userLoginViewModel { get; set; }
        public Token tokenViewModel { get; set; }

        public MainModel()
        {
            new Token();
            new UserLogin();
        }
    }
}