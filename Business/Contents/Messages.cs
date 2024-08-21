using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contents
{
    public static class Messages
    {

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string UserRegistered = "Kullanici kayıtlı";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Sifre hatalı";
        public static string SuccessfulLogin = "Başarılı Giriş";
        public static string UserAlreadyExists = "Kullanıcı zaten var";
        public static string AccessTokenCreated = "Erişim tokeni Oluşturuldu";

        public static string TimetableAdded = "Ders programı ekleme işlemi başarlılı";

        public static string ScheduleSttingAdded = "Ders ayarları ekleme başarılı";

        public static string ClassAdded = "Sınıf ekleme işlemi başarlıl";

        public static string AcademicAdded = "Akademisyen ekleme işlemi başarılı";

        public static string CoursesAdded = "Ders ekleme işlemi başarılı";

        public static string ClassCourseAdded = "Ders sınıf bağlama işlemi başarlı";

        public static string weeklyEnd = "Hafta bitti";
        public static string TimetableAllDeleted = "Toplu silme işlemi başarılı";

        public static string ScheduleSttingUpdated = "Ders programı ayarı değiştirildi";

        public static string ClassCourseTotalLimited = "Toplam ders kredi sayısı bu ders ayarına aykırı";

        public static string timetableFull = "Ders programı tablosu zaten dolu";

        public static string timeTableClassDeleted = "İstenen ders silindi";

        public static string TimetableClassAdded = "İstenen sınıfa ders eklendi";

        public static string ScheduleSttingDontAdded = "Ders ayar tablosu var olduğu için eklenemedi";

        public static string ClassNotFind = "Sınıf bulunamadı";
        public static string ScheduleSttingDontFind = "Ders ayarı bulunamadı ayar ekleyiniz";

        public static string ClassNameFind = "Aynı isimde zaten isim bulunmakta";

        public static string ClassDeleted = "Sınıf silme işlemi başarılı";
        public static string ClassUpdate = "Sınıf güncelleme işlemi başarılı";

        public static string ClassStudents = "Sınıf silmek için ilk önce sınıfa bağlı öğrencileri siliniz";

        public static string StudentsModified = "Öğrenci güncelleme işlemi başarlı";
        public static string StudentsAdded = "Öğrenci ekleme işlemi başarılı";

        public static string StudentsDelete = "Öğrenci silme işlemi başarılı";

        public static string StudentsClassAllDelete = "Sınıftaki tüm öğrenciler temizlendi";

        public static string CoursesDeleted = "Ders silindi";

        public static string CoursesClassAdded = "Ders sınıf bağlantısı yapıldı";
        public static string CoursesClass = "Lütfen ilk önce ders sınıf bağlatısını kaldırınız";

        public static string ClassCourseDeleted = "Sınıf ders bağlantısı silindi";

        public static string ClassAllCourseAdded = "İstenen sınıfa tüm dersler atandı";

        public static string AllClassCourse = "Tüm sınıflar ve derslerin bağlantısı yapıldı";

        public static string AcademicDeleted = "Akademisyen silindi";
        public static string AcademicModified = "Akademisyen güncellendi";

        public static string NotInput = "Gönderilen değer alınamadı";

        public static string ClassCourseFind = "Zaten atanmış bir ders";
    }
}
