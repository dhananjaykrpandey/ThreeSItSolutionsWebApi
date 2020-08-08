using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeSItSolutionsWebApi.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace ThreeSItSolutionsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //    [EnableCors("MyPolicy")]
    public class ContactUsController : ControllerBase
    {
        private readonly Db3SItSoultion _context;

        public ContactUsController(Db3SItSoultion context)
        {
            _context = context;
        }

        // GET: api/ContactUs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MContactUs>>> GetMContactUs()
        {
            return await _context.MContactUs.ToListAsync();
        }

        // GET: api/ContactUs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MContactUs>> GetMContactUs(int id)
        {
            var mContactUs = await _context.MContactUs.FindAsync(id);

            if (mContactUs == null)
            {
                return NotFound();
            }

            return mContactUs;
        }

        // PUT: api/ContactUs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMContactUs(int id, MContactUs mContactUs)
        {
            if (id != mContactUs.IID)
            {
                return BadRequest();
            }

            _context.Entry(mContactUs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MContactUsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContactUs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MContactUs>> PostMContactUs([FromBody] MContactUs mContactUs)
        {
            if (mContactUs.CName== null)
            {
                return BadRequest();
            }
            else if (mContactUs.CEmailId == null)
            {
                return BadRequest();
            }
            else if (mContactUs.CSubject == null)
            {
                return BadRequest();
            }
            else if (mContactUs.CMessage == null)
            {
                return BadRequest();
            }

            _context.MContactUs.Add(mContactUs);
            await _context.SaveChangesAsync();
            //SendMail(mContactUs.CEmailId, mContactUs.CSubject, mContactUs.CMessage, mContactUs.CName);

            return CreatedAtAction("GetMContactUs", new { id = mContactUs.IID }, mContactUs);
        }

        // DELETE: api/ContactUs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MContactUs>> DeleteMContactUs(int id)
        {
            var mContactUs = await _context.MContactUs.FindAsync(id);
            if (mContactUs == null)
            {
                return NotFound();
            }

            _context.MContactUs.Remove(mContactUs);
            await _context.SaveChangesAsync();

            return mContactUs;
        }

        private bool MContactUsExists(int id)
        {
            return _context.MContactUs.Any(e => e.IID == id);
        }
        public static void SendMail(string CCAddress, string Subject, string BodyContent, string Name)
        {
            try
            {
                //From Address    
                string FromAddress = "donotreply@3s-itsolutions.co.za";
                string FromAdressTitle = "Do Not Reply";
                //To Address    
                string ToAddress = "info@3s-itsolutions.co.za";
                string ToAdressTitle = "3s-itsolutions Information Desk";
                //string Subject = "Tesing";
                //string BodyContent = "Testing";

                //Smtp Server    
                string SmtpServer = "cp20.domains.co.za";
                //Smtp Port Number    
                int SmtpPortNumber = 465;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (FromAdressTitle,
                                         FromAddress
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (ToAdressTitle,
                                         ToAddress
                                         ));
                //mimeMessage.To.Add(new MailboxAddress
                //                       (Name,
                //                        CCAddress
                //                        ));
                mimeMessage.Subject = string.Concat("Contact Us Message", " - ", Subject); //Subject  

                string StrMessage = MessageBody(Subject, BodyContent, Name, CCAddress);
                var builder = new BodyBuilder();
                builder.HtmlBody = StrMessage;
                mimeMessage.Body = builder.ToMessageBody();


                using var client = new SmtpClient();
                client.Connect(SmtpServer, SmtpPortNumber, MailKit.Security.SecureSocketOptions.Auto);
                client.Authenticate(
                    "donotreply@3s-itsolutions.co.za",
                    "Santosh$1982"
                    );
                client.Send(mimeMessage);
                //Console.WriteLine("The mail has been sent successfully !!");
                //Console.ReadLine();
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static string MessageBody(string Subject, string BodyContent, string Name, string Email)
        {
            string MessageBoday = $@"
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http - equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title> 3S IT Training and Business Solutions</title>
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
</head>
<body style='margin: 0; padding: 0;'>
    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
        <tr>
            <td style='padding: 10px 0 30px 0;'>
                <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'
                    style='border: 1px solid #cccccc; border-collapse: collapse;'>
                    <tr>
                        <td align='center' bgcolor='#ee4c50'
                            style='padding: 40px 0 30px 0; color: #153643; font-size: 32px; font - weight: bold; font - family: Arial, sans - serif; color:#ffffff'>
                            3S IT Training and Business Solutions
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor='#ffffff' style='padding: 40px 30px 40px 30px;'>
                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                <tr>
                                    <td style='color: #153643; font-family: Arial, sans-serif; font-size: 24px;'>
                                        <b> Message From Contact Us </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Sender Name :- { (object)Name} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Sender Email :- { (object)Email} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                            <tr>
                                                <td width='260' valign='top'>
                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                        <tr>
                                                            <td
                                                                style='color: #153643; font-family: Arial, sans-serif; font-size: 24px;'>
                                                                Sender Subject :- { (object)Subject}
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td
                                                                style='padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                                                Sender Message:- { (object)BodyContent}
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='font-size: 0; line-height: 0;' width='20'>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor='#ee4c50' style='padding: 30px 30px 30px 30px;'>
                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                <tr>
                                    <td style='color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;'
                                        width='75%'>
                                        Mail Us &#64; 3S IT Training and Business Solutions [ info@3s-itsolutions.co.za
                                        ]
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html> ";

            return MessageBoday;
        }


    }
}
