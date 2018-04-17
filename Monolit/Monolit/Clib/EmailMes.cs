using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Monolit.Models;

namespace Monolit.Clib
{
    public class EmailMes
    {
        public void SentEmail(Messager Model)
        {

            try
            {

                //Дублируем в базе данных
 //               WorkData wd = new WorkData();
  //              wd.AddMessageBd(Model);
                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress("info@magistral-40.ru", "Монолит");
                // кому отправляем
                MailAddress to = new MailAddress("monolit40@bk.ru");
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Обратная связь с сайта Монолит";
                // текст письма
                m.Body = "<p><strong>" + Model.UserName + "</strong> отправил заявку </p><p><strong>Email: </strong>" + Model.EmailAdrress + " <strong>Телефон: </strong>" + Model.Phone + "</p><p><strong>Сообщение:</strong> " + Model.Message + "</p>";
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("mail.monolit40.ru", 25);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("info@monolit40.ru", "j5l0mF~1");
                smtp.EnableSsl = false;
                smtp.Send(m);

            }

            catch (Exception ex) { }
        }
    }
}