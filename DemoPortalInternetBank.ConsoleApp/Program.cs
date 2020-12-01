using System;
using System.IO;
using DemoPortalInternetBank.Pki;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Utilities.IO.Pem;

namespace DemoPortalInternetBank.ConsoleApp
{
    class Program
    {
        private static string request = @"-----BEGIN NEW CERTIFICATE REQUEST-----
MIICvDCCAiUCAQAwTDEOMAwGA1UEAwwFYWRtaW4xDjAMBgNVBAMMBVVzZXJzMRYw
FAYKCZImiZPyLGQBGRYGdGVzdGNhMRIwEAYKCZImiZPyLGQBGRYCcnUwgZ8wDQYJ
KoZIhvcNAQEBBQADgY0AMIGJAoGBAO4DsygJLrKNxlCrdjXdg4q/4757VZqWEMet
KmHNfwmncELvjMPTmmQSojZqcgu8s4MEYimTojxOE4UKiCrdmimXZEyF4Y6ooA/r
vt0MO2Nx9R9dTUppsTIuw9u0GfEBZxxdh9XhI05mZfbBjazu4AAHg4Qq8iZGDopv
MZ/0RtCdAgMBAAGgggEuMBwGCisGAQQBgjcNAgMxDhYMMTAuMC4xNDM5My4yME0G
CSsGAQQBgjcVFDFAMD4CAQkMDXJlcS50ZXN0Y2EucnUMIVRFU1RDQVzQsNC00LzQ
uNC90LjRgdGC0YDQsNGC0L7RgAwHY2VydHJlcTBTBgkqhkiG9w0BCQ4xRjBEMA4G
A1UdDwEB/wQEAwIFoDATBgNVHSUEDDAKBggrBgEFBQcDATAdBgNVHQ4EFgQUxDcK
9RVAGOqyPiMyjYAp3CbFhvkwagYKKwYBBAGCNw0CAjFcMFoCAQEeUgBNAGkAYwBy
AG8AcwBvAGYAdAAgAEIAYQBzAGUAIABTAG0AYQByAHQAIABDAGEAcgBkACAAQwBy
AHkAcAB0AG8AIABQAHIAbwB2AGkAZABlAHIDAQAwDQYJKoZIhvcNAQEFBQADgYEA
bl2YJLuhcOKI1w+X7UTetHUO/dWiCEgSwRvYaQ7T9yoZtOPa/SIfiTasOl9Ilhjl
tWohPC6qa/hWhzgi1ZxPKE+ck1vHzN7j/FnlYnKSos39Vs4vhA/A6g0vfOfKSmUD
eICvLrmN/N6mSWIvJ/8mmBKuqBvVr9yrQ9/9H9qY7bs=
-----END NEW CERTIFICATE REQUEST-----
";
        static void Main(string[] args)
        {
            
            var pkiManager = new PkiManager();

            var result = pkiManager.CreateCrl();
            
            var pem = GetPem(result.GetEncoded(), "CRL");
            
            Console.WriteLine(pem);

//            var pkiManager = new PkiManager();
//            
//            var result = pkiManager.IssueCertificate(request, new DefaultExtensionBuilder());
//
//            var pem = GetPem(result.GetEncoded(), "CERTIFICATE");
//            
//            Console.WriteLine(pem);
        }

        private static void getSelSigned()
        {
            var selfSigned = new GOSTPkiService().GenerateSelfSigned();

            var keyPair = selfSigned.Item1;

            var infoPrivate = PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair.Private);
            
            var serializedPrivateKey = infoPrivate.GetDerEncoded();

            var priv = GetPem(serializedPrivateKey, "PRIVATE KEY");

            Console.WriteLine(priv);
            
            var certificate = selfSigned.Item2.GetEncoded();

            var cert = GetPem(certificate, "CERTIFICATE");

            Console.WriteLine(cert);
        }

        public static string GetPem(byte[] encoded, String type)
        {
            var stringWriter = new StringWriter();
            var pemWriter = new PemWriter(stringWriter);
            var pemObject = new PemObject(type, encoded);
            
            pemWriter.WriteObject(pemObject);

            var res = stringWriter.ToString();

            return res;
        }
    }
}