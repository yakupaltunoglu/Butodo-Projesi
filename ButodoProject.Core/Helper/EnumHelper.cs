using ButodoProject.Core.Model.FixType;
using System;
using System.Collections.Generic;

namespace ButodoProject.Core.Helper
{
    public static class EnumHelper
    {
        public static string RolePageTypeName(RolePageType status)
        {
            switch (status)
            {
                case RolePageType.CompanyList: return "Şirketler";
                case RolePageType.CompanyAddorEdit: return "Şirket ekle veya düzenle";
                case RolePageType.PersonalList: return "Personeller";
                case RolePageType.PersonalAddorEdit: return "Personel ekle veya düzenle";
                case RolePageType.PersonalProjectList: return "Personel projeler";
                case RolePageType.PersonalProjectAddorEdit: return "Personel proje ekle veya düzenle";
                case RolePageType.ProjectList: return "Projeler";
                case RolePageType.ProjectAddorEdit: return "Proje ekle veya düzenle";
                case RolePageType.SpendTimeList: return "Harcanan dakikalar";
                case RolePageType.SpendTimeAddorEdit: return "Harcanan dakika ekle veya düzenle";
                case RolePageType.TaskMessageList: return "Görev mesajları";
                case RolePageType.TaskMessageAddorEdit: return "Görev mesajı ekle veya düzenle";
                case RolePageType.TaskTableList: return "Görevler";
                case RolePageType.TaskTableAddorEdit: return "Görev ekle veya düzenle";
                default: return "[" + status.ToString() + "]";
            }
        }
        public static string RoleTypeName(RoleType status)
        {
            switch (status)
            {
                case RoleType.Blocked: return "Engelli";
                case RoleType.Owner: return "Düzenleyebilir";
                case RoleType.Viewer: return "Görebilir";
                default: return "[" + status.ToString() + "]";
            }
        }
        public static IList<RolePageType> GetRolePageTypes()
        {
            var list = new List<RolePageType>();
            foreach (RolePageType test in Enum.GetValues(typeof(RolePageType)))
            {
                if (test == RolePageType.None) continue;
                list.Add(test);
            }

            return list;
        }
        public static IList<RoleType> GetRoleTypes()
        {
            var list = new List<RoleType>();
            foreach (RoleType test in Enum.GetValues(typeof(RoleType)))
            {
                list.Add(test);
            }
            return list;
        }

        public static string GetName(this RolePageType status)
        {
            return RolePageTypeName(status);
        }
        public static string GetName(this RoleType status)
        {
            return RoleTypeName(status);
        }

    }
}