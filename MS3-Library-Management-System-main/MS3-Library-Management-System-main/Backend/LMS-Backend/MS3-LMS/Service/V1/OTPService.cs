using MS3_LMS.Enity.Notification;
using System;
using System.Linq;
using System.Threading.Tasks;

using MS3_LMS.LMSDbcontext;
using Microsoft.EntityFrameworkCore;

public class OTPService
{
    private readonly LMSContext _context;
    private readonly EmailService _emailService;

    public OTPService(LMSContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public string GenerateOtp()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString(); 
    }

    public async Task<bool> GenerateAndSendOtpAsync(Guid userId, string email)
    {
        try
        {
           
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new InvalidOperationException("User does not exist.");
            }

           
            var otpCode = GenerateOtp();

            var otp = new OTP
            {
                Id = Guid.NewGuid(),
                OTPCode = otpCode,
                ExpiryTime = DateTime.UtcNow.AddMinutes(5),
                UserId = userId,
            };
            _context.OTPs.Add(otp);
            await _context.SaveChangesAsync();
            var subject = "Your OTP for Registration";
            var body = $"Your OTP Code is: <b>{otpCode}</b>. It will expire in 5 minutes.";

            if (await _emailService.SendEmailAsync(email, subject, body))
            {
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }



    }


    public async Task<bool> VerifyOtpAsync(Guid userId, string otpCode)
    {
        try
        {
            
            var otp = await _context.OTPs.FirstOrDefaultAsync(o => o.UserId == userId && o.OTPCode == otpCode);

            if (otp == null)
            {
                throw new InvalidOperationException("Invalid OTP.");
            }

           
            if (otp.ExpiryTime < DateTime.UtcNow)
            {
                throw new InvalidOperationException("OTP has expired.");
            }

           
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            user.IsConfirmEmail = true; 

            
            _context.OTPs.Remove(otp);

            
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
           
            Console.WriteLine($"Error verifying OTP: {ex.Message}");
            return false;
        }
    }




}
