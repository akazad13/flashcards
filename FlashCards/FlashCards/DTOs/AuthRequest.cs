﻿using System.ComponentModel.DataAnnotations;

namespace FlashCards.DTOs
{
    public class AuthRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
