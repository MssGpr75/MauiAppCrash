using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MauiAppCrash
{
	internal class SecurityHelper
	{
		public SecurityHelper()
		{
		}

		public bool RASSignXml(ref XmlDocument doc, out string debug)
		{
			debug = "";

			RSA rsaKey = null;

			try
			{
				debug += "Create RSA" + Environment.NewLine;
				rsaKey = RSA.Create();
			}
			catch(Exception ex)
			{
				debug += $"{ex.GetType().Name}{Environment.NewLine}{ex}";
				return false; 
			}

			SignedXml signedXml = new(doc) {
				SigningKey = rsaKey
			};

			try
			{
				debug += "Creating reference" + Environment.NewLine;

				// Create a reference to be signed.
				Reference reference = new() {
					Uri = ""
				};
				XmlDsigEnvelopedSignatureTransform env = new();

				debug += "Add transform" + Environment.NewLine;
				reference.AddTransform(env);

				debug += "Add reference" + Environment.NewLine;
				signedXml.AddReference(reference);

				debug += "Compute signature" + Environment.NewLine;
				signedXml.ComputeSignature();

				debug += "Get Xml" + Environment.NewLine;
				XmlElement xmlDigitalSignature = signedXml.GetXml();

				doc.DocumentElement?.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
			}
			catch (Exception ex)
			{
				debug += $"{ex.GetType().Name}{Environment.NewLine}{ex}";
				return false;
			}

			return true;
		}
	}
}
