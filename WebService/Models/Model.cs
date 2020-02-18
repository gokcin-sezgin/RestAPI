using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class Model
    {
    }
    public class UserContext : DbContext
    {
        
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }
    

        public DbSet<UserInfo> UserInfos { get; set; }
        
    }

    public class UserInfo
    {
        [Key]
        public int UserInfoId { get; set; }

        [Display(Name = "Ad")]
        [Required]
        public string PersonelAd { get; set; }

        [Display(Name = "Soyad")]

        public string PersonelSoyad { get; set; }

        [Display(Name = "TC Numarası")]
      // [Range(11,12,ErrorMessage ="TC Kimlik Numarası uzunluğu 11 olmalı!")]
        public long PersonelTcNo { get; set; }

        //[Range(6,16,ErrorMessage ="Kullanıcı adı 6-15 arası uzunluğunda olmalı!")]

        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        
        //[Range(6,16,ErrorMessage ="Şifre 6-15 arası uzunluğunda olmalı!")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    } 

    public class Admin
    {
        public static string username = "admin";

        public static string password = "123456";
    }
    
}
