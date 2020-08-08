using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class MEnquiriesController : ControllerBase
    {
        private readonly Db3SItSoultion _context;

        public MEnquiriesController(Db3SItSoultion context)
        {
            _context = context;
        }

        // GET: api/MEnquiries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MEnquiry>>> GetMEnquiry()
        {
            return await _context.MEnquiry.ToListAsync();
        }

        // GET: api/MEnquiries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MEnquiry>> GetMEnquiry(int id)
        {
            var mEnquiry = await _context.MEnquiry.FindAsync(id);

            if (mEnquiry == null)
            {
                return NotFound();
            }

            return mEnquiry;
        }

        // PUT: api/MEnquiries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMEnquiry(int id, MEnquiry mEnquiry)
        {
            if (id != mEnquiry.IID)
            {
                return BadRequest();
            }

            _context.Entry(mEnquiry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MEnquiryExists(id))
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

        // POST: api/MEnquiries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MEnquiry>> PostMEnquiry([FromBody] MEnquiry mEnquiry)
        {
            _context.MEnquiry.Add(mEnquiry);
            await _context.SaveChangesAsync();
            SendMail(mEnquiry);
            return CreatedAtAction("GetMEnquiry", new { id = mEnquiry.IID }, mEnquiry);
        }

        // DELETE: api/MEnquiries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MEnquiry>> DeleteMEnquiry(int id)
        {
            var mEnquiry = await _context.MEnquiry.FindAsync(id);
            if (mEnquiry == null)
            {
                return NotFound();
            }

            _context.MEnquiry.Remove(mEnquiry);
            await _context.SaveChangesAsync();

            return mEnquiry;
        }

        private bool MEnquiryExists(int id)
        {
            return _context.MEnquiry.Any(e => e.IID == id);
        }
        public static void SendMail(MEnquiry mEnquiry)
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
                mimeMessage.Subject = string.Concat("Enquiry From", " - ", mEnquiry.cFullName); //Subject  

                string StrMessage = MessageBody(mEnquiry);
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
        public static string MessageBody(MEnquiry mEnquiry)
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
                                        <b> Enquiry From </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Name :- { (object)mEnquiry.cFullName} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Email :- { (object)mEnquiry.cEmailId} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Mobile No :- { (object)mEnquiry.cMobileNo} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Phone No :- { (object)mEnquiry.cPhoneNo} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Date of Birth :- { (object)mEnquiry.dDob} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Gender :- { (object)mEnquiry.cGender} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Guardian :- { (object)mEnquiry.cGuradian} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Company :- { (object)mEnquiry.cCompnay} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Academic :- { (object)mEnquiry.cAcademic} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        CareerBackground :- { (object)mEnquiry.cCareerBackground} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        CurrentOccupation :- { (object)mEnquiry.cCurrentOccupation} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        School :- { (object)mEnquiry.cSchaool} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Reason :- { (object)mEnquiry.cReason} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        SourceOfInfo :- { (object)mEnquiry.cSourceOfInfo} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Remarks :- { (object)mEnquiry.cRemarks} <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                        Enquiry Date :- { (object)mEnquiry.DCreateDate} <br />
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

</html>";

            return MessageBoday;
        }
    }
}
