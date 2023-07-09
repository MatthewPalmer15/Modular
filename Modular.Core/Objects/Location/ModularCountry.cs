using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;

namespace Modular.Core
{
    public class Country : ModularBase
    {

        #region "  Static Guids  "

        // A
        public static readonly Guid MODULAR_COUNTRY_ID_AFGHANISTAN = new Guid("8EA0BC21-2F02-4C11-A24B-234B503CE0C5");
        public static readonly Guid MODULAR_COUNTRY_ID_ALAND_ISLANDS = new Guid("AD941EB0-6784-43FC-9AFB-4015D2D43139");
        public static readonly Guid MODULAR_COUNTRY_ID_ALBANIA = new Guid("1999BD61-5AF1-4529-B573-339CF5DD02EA");
        public static readonly Guid MODULAR_COUNTRY_ID_ALGERIA = new Guid("FD692139-DB4B-4B1D-8C8E-9812CC402241");
        public static readonly Guid MODULAR_COUNTRY_ID_AMERICAN_SAMOA = new Guid("ADD12370-689D-4EB6-A2D2-6FF2506EB578");
        public static readonly Guid MODULAR_COUNTRY_ID_ANDORRA = new Guid("E902A696-AB52-4155-9918-AB69310D1014");
        public static readonly Guid MODULAR_COUNTRY_ID_ANGOLA = new Guid("F37D0784-3B2F-494C-80AD-DAD8F14E117D");
        public static readonly Guid MODULAR_COUNTRY_ID_ANGUILLA = new Guid("FC244BD6-16B6-4B0B-8188-1E30B48E66EA");
        public static readonly Guid MODULAR_COUNTRY_ID_ANTARCTICA = new Guid("0C8096BE-ECFC-467D-BE49-1FED3C3A087A");
        public static readonly Guid MODULAR_COUNTRY_ID_ANTIGUA_AND_BARBUDA = new Guid("4FDA595C-1758-4DF5-A3CB-EB1DC50ACBD8");
        public static readonly Guid MODULAR_COUNTRY_ID_ARGENTINA = new Guid("BCEEF4B7-41F2-45CF-BB32-D7F643F8E322");
        public static readonly Guid MODULAR_COUNTRY_ID_ARMENIA = new Guid("C0D7DF26-36CC-4549-92FD-B952E451C7A0");
        public static readonly Guid MODULAR_COUNTRY_ID_ARUBA = new Guid("E6D665CF-99CA-45D4-838E-2617FAEE296F");
        public static readonly Guid MODULAR_COUNTRY_ID_AUSTRALIA = new Guid("14A8DF68-BCBD-494C-B126-14ADCCEAF507");
        public static readonly Guid MODULAR_COUNTRY_ID_AUSTRIA = new Guid("AD16F5E7-9680-465E-B63C-52F5CE83569D");
        public static readonly Guid MODULAR_COUNTRY_ID_AZERBAIJAN = new Guid("B6FE255D-3799-46B7-BB64-7D0B87735D33");

        // B
        public static readonly Guid MODULAR_COUNTRY_ID_BAHAMAS = new Guid("E5FE907D-0595-465D-9469-F9250141EF59");
        public static readonly Guid MODULAR_COUNTRY_ID_BAHRAIN = new Guid("65C1F1B6-5B28-4489-B16A-0EDB5D83A800");
        public static readonly Guid MODULAR_COUNTRY_ID_BANGLADESH = new Guid("991FC808-6FF5-4988-AEEE-DDBE2B6F310F");
        public static readonly Guid MODULAR_COUNTRY_ID_BARBADOS = new Guid("ACC5F8BE-B486-4AC7-A84F-C70FCB67EFB5");
        public static readonly Guid MODULAR_COUNTRY_ID_BELARUS = new Guid("3435521C-117C-455A-94CE-0FA83D76FC33");
        public static readonly Guid MODULAR_COUNTRY_ID_BELGIUM = new Guid("54C570F5-1F24-4A88-A28E-6CA2FE2CD7F6");
        public static readonly Guid MODULAR_COUNTRY_ID_BELIZE = new Guid("C168124D-B843-42C9-BA67-A9C4A867F4FE");
        public static readonly Guid MODULAR_COUNTRY_ID_BENIN = new Guid("E0BB3CC0-4935-4674-B26F-80903B3A2DF0");
        public static readonly Guid MODULAR_COUNTRY_ID_BERMUDA = new Guid("A38F5FE8-436C-4A7C-9526-38DA55869054");
        public static readonly Guid MODULAR_COUNTRY_ID_BHUTAN = new Guid("DEAFCF9B-ECF7-47C2-B67D-11C36A569CC5");
        public static readonly Guid MODULAR_COUNTRY_ID_BOSNIA_HERZEGOVINA = new Guid("30410F60-5027-403D-9EB5-7D8FC8B8EC11");
        public static readonly Guid MODULAR_COUNTRY_ID_BOTSWANA = new Guid("C8D55222-FB4C-4918-9ACA-BC8D37A830F5");
        public static readonly Guid MODULAR_COUNTRY_ID_BOUVET_ISLAND = new Guid("E4525E2B-4D5F-4984-BB42-33D1B541302D");
        public static readonly Guid MODULAR_COUNTRY_ID_BRAZIL = new Guid("61B3B991-5957-465D-9518-00E78C5F9A0B");
        public static readonly Guid MODULAR_COUNTRY_ID_BRITISH_INDIAN_OCEAN_TERRITORY = new Guid("48CFDE8D-AEA7-45A2-A878-9FB89B3FD59E");
        public static readonly Guid MODULAR_COUNTRY_ID_BRUNEI_DARUSSALAM = new Guid("56CFA98A-2AB2-4311-8C22-27A10B2D3F85");
        public static readonly Guid MODULAR_COUNTRY_ID_BULGARIA = new Guid("EB8790A8-ECDD-404A-A07B-87E112CD0430");
        public static readonly Guid MODULAR_COUNTRY_ID_BURKINA_FASO = new Guid("5622D8BA-9DA1-43A1-A8BF-650002A2AF33");
        public static readonly Guid MODULAR_COUNTRY_ID_BURUNDI = new Guid("BD7A11AB-1E7A-4CC1-BF18-4270B8B9CD5C");

        // C
        public static readonly Guid MODULAR_COUNTRY_ID_CAMBODIA = new Guid("A04EC901-22D0-4854-90D1-664FBC0105CB");
        public static readonly Guid MODULAR_COUNTRY_ID_CAMEROON = new Guid("8A1F9893-D664-4C2F-9FC8-20BF77E8DD1A");
        public static readonly Guid MODULAR_COUNTRY_ID_CANADA = new Guid("4B75565F-374A-4045-A5CA-8F529FFF37C1");
        public static readonly Guid MODULAR_COUNTRY_ID_CAPE_VERDE = new Guid("142973A6-75D5-49E4-A420-DEBBCB8C3D75");
        public static readonly Guid MODULAR_COUNTRY_ID_CAYMAN_ISLANDS = new Guid("7A998111-DE0A-4E82-A65D-EE7A5CDC5B0E");
        public static readonly Guid MODULAR_COUNTRY_ID_CENTRAL_AFRICAN_REPUBLIC = new Guid("1D912039-0370-4F31-BA0D-380CF17EF633");
        public static readonly Guid MODULAR_COUNTRY_ID_CHILE = new Guid("6B2A74B5-3EE5-4330-86A3-03DC77B0E67B");
        public static readonly Guid MODULAR_COUNTRY_ID_CHINA = new Guid("1D6050AD-7DBE-48A6-ABE6-65147584C7B2");
        public static readonly Guid MODULAR_COUNTRY_ID_CHRISTMAS_ISLAND = new Guid("85FE4DC2-774A-4123-ADD4-5DDFB82E2A95");
        public static readonly Guid MODULAR_COUNTRY_ID_COCOS_ISLANDS = new Guid("FFDA6258-7A55-43D2-8E9B-AB95230F7CAC");
        public static readonly Guid MODULAR_COUNTRY_ID_COLOMBIA = new Guid("D49623C2-8741-4B78-BFBB-2867CEA15B38");
        public static readonly Guid MODULAR_COUNTRY_ID_COMOROS = new Guid("DD02383C-1B87-40C1-B34C-2E6DE47B65C1");
        public static readonly Guid MODULAR_COUNTRY_ID_CONGO_BRAZZAVILLE = new Guid("872AD9F0-C617-4661-AB79-583B008A7C74");
        public static readonly Guid MODULAR_COUNTRY_ID_CONGO_KINSHASA = new Guid("051846C4-3C2C-4163-B47D-74E12BF77404");
        public static readonly Guid MODULAR_COUNTRY_ID_COOK_ISLANDS = new Guid("01CC0AAE-8906-4AF0-9B51-580A4FF24563");
        public static readonly Guid MODULAR_COUNTRY_ID_COSTA_RICA = new Guid("9BCABDB7-4672-4365-B91B-F53480D09F5A");
        public static readonly Guid MODULAR_COUNTRY_ID_COTE_D_IVOIRE = new Guid("3C39DC42-7276-4EBC-B12A-A38F96275833");
        public static readonly Guid MODULAR_COUNTRY_ID_CROATIA = new Guid("85C797CE-56F3-4167-9F8B-809A636150BC");
        public static readonly Guid MODULAR_COUNTRY_ID_CUBA = new Guid("84E1DA39-AF8C-49CE-A4D8-B2B34DD02641");
        public static readonly Guid MODULAR_COUNTRY_ID_CYPRUS = new Guid("35B2F8ED-281F-42A9-B103-9394A0527CAD");
        public static readonly Guid MODULAR_COUNTRY_ID_CZECH_REPUBLIC = new Guid("866290C9-C463-4DC1-B6C6-50BA249D6E48");

        // D
        public static readonly Guid MODULAR_COUNTRY_ID_DENMARK = new Guid("2A0058DE-E1DB-4FAD-83B9-181D006C53BD");
        public static readonly Guid MODULAR_COUNTRY_ID_DJIBOUTI = new Guid("6439E4CA-35ED-4189-B922-6BC9F5E4BDED");
        public static readonly Guid MODULAR_COUNTRY_ID_DOMINICA = new Guid("0061FAD5-6219-4947-9679-1BB493FABFA2");
        public static readonly Guid MODULAR_COUNTRY_ID_DOMINICAN_REPUBLIC = new Guid("80C64C62-637E-4827-9573-E81E35FD8725");

        // E
        public static readonly Guid MODULAR_COUNTRY_ID_ECUADOR = new Guid("39AF74EC-E0C1-4093-AF91-052754604CDB");
        public static readonly Guid MODULAR_COUNTRY_ID_EGYPT = new Guid("8166763A-75B7-4E2D-A30B-2A6458719F79");
        public static readonly Guid MODULAR_COUNTRY_ID_EL_SALVADOR = new Guid("24A1A8EE-1577-4B21-B730-BCFC629B27F1");
        public static readonly Guid MODULAR_COUNTRY_ID_ERITREA = new Guid("FC4E6BEB-8E3D-4957-B17F-67429A407C18");
        public static readonly Guid MODULAR_COUNTRY_ID_ESTONIA = new Guid("8D402C72-E17B-4E99-878E-E37EFA30A382");
        public static readonly Guid MODULAR_COUNTRY_ID_ETHIOPIA = new Guid("2F007834-E60F-444B-A2C4-72A4597BA4EF");

        // F
        public static readonly Guid MODULAR_COUNTRY_ID_FALKLAND_ISLANDS = new Guid("DABD181F-4910-459D-8177-02243A521EE5");
        public static readonly Guid MODULAR_COUNTRY_ID_FAROE_ISLANDS = new Guid("27233C48-36A2-4B8B-B588-FDFDB910EF05");
        public static readonly Guid MODULAR_COUNTRY_ID_FIJI = new Guid("9BCD5FC8-CB66-45D6-93D6-1A85AA556531");
        public static readonly Guid MODULAR_COUNTRY_ID_FINLAND = new Guid("769D4DE8-157C-44C8-A5AE-15293BC87563");
        public static readonly Guid MODULAR_COUNTRY_ID_FRANCE = new Guid("CE30B7A1-E83D-4978-B512-30333435B57F");
        public static readonly Guid MODULAR_COUNTRY_ID_FRENCH_GUIANA = new Guid("537FC444-214B-48AC-BFD8-E476555251CB");
        public static readonly Guid MODULAR_COUNTRY_ID_FRENCH_POLYNESIA = new Guid("71D0C714-A228-41DA-A76F-94CB63C7A895");
        public static readonly Guid MODULAR_COUNTRY_ID_FRENCH_SOUTHERN_TERRITORIES = new Guid("5F327B03-DCA6-4C73-BD56-8ABE1B00C06C");

        // G
        public static readonly Guid MODULAR_COUNTRY_ID_GABON = new Guid("905F5902-6578-46F1-A5F8-B5B28312C1CF");
        public static readonly Guid MODULAR_COUNTRY_ID_GAMBIA = new Guid("34D1FD41-C2CD-45B6-BA2D-4581E80BCAF7");
        public static readonly Guid MODULAR_COUNTRY_ID_GEORGIA = new Guid("44F0832B-52F5-42F2-9775-BC5EFEF70B17");
        public static readonly Guid MODULAR_COUNTRY_ID_GERMANY = new Guid("7AC023F2-59C0-489F-9D6D-C6B8CA4273CE");
        public static readonly Guid MODULAR_COUNTRY_ID_GHANA = new Guid("07D81209-0184-4B07-ACB0-C21FEFF1404E");
        public static readonly Guid MODULAR_COUNTRY_ID_GIBRALTAR = new Guid("8317441F-671D-4BA4-959A-AB260AB99C6D");
        public static readonly Guid MODULAR_COUNTRY_ID_GREECE = new Guid("F3E5E77F-2C53-4457-B9F9-88125B36B526");
        public static readonly Guid MODULAR_COUNTRY_ID_GREENLAND = new Guid("6F551CC3-81AE-4D4B-8030-1A3C0475D3D1");
        public static readonly Guid MODULAR_COUNTRY_ID_GRENADA = new Guid("B9C20483-FD10-428C-A67E-7282E95A5638");
        public static readonly Guid MODULAR_COUNTRY_ID_GUADELOUPE = new Guid("E1287683-BCC4-4FC6-A101-0240A74ED094");
        public static readonly Guid MODULAR_COUNTRY_ID_GUAM = new Guid("73960A75-5A8C-4487-B2E1-010A5FDA71EF");
        public static readonly Guid MODULAR_COUNTRY_ID_GUERNSEY = new Guid("0339F336-EBF5-4A8D-A51E-2FABB63662A5");
        public static readonly Guid MODULAR_COUNTRY_ID_GUINEA = new Guid("488DA9A1-D318-4803-A4FE-7104C09AB25E");
        public static readonly Guid MODULAR_COUNTRY_ID_GUINEA_BISSAU = new Guid("687895F0-1BD0-4AD1-A803-4F3128153FC4");
        public static readonly Guid MODULAR_COUNTRY_ID_GUYANA = new Guid("BFED0199-DC22-4B2E-BCA5-5EF49C589C7B");

        // H
        public static readonly Guid MODULAR_COUNTRY_ID_HEARD_ISLAND_AND_MCDONALD_ISLANDS = new Guid("4EE91615-A169-4D55-B37C-3E657585F476");
        public static readonly Guid MODULAR_COUNTRY_ID_HONDURAS = new Guid("DD765FA8-CD62-428F-BE32-3F8FCA23C159");
        public static readonly Guid MODULAR_COUNTRY_ID_HONG_KONG = new Guid("D6A284CE-F1A0-4B1C-92CA-F4EC5EE4AC47");
        public static readonly Guid MODULAR_COUNTRY_ID_HUNGARY = new Guid("48283928-DBB9-45CE-8508-BB3B7F7B70C2");

        // I
        public static readonly Guid MODULAR_COUNTRY_ID_ICELAND = new Guid("82745217-1B34-46C2-8D16-FBAA722B9581");
        public static readonly Guid MODULAR_COUNTRY_ID_INDIA = new Guid("4761E4F9-4303-4BF7-B817-4E74625D61FD");
        public static readonly Guid MODULAR_COUNTRY_ID_INDONESIA = new Guid("0466B053-A760-4996-A654-9A8C30F95112");
        public static readonly Guid MODULAR_COUNTRY_ID_IRAN = new Guid("5D0F9D17-BF43-4DBE-84C9-B9F94183C9AD");
        public static readonly Guid MODULAR_COUNTRY_ID_IRAQ = new Guid("4F59B72A-2AF3-4087-AAD7-D0B9AC8A7C2B");
        public static readonly Guid MODULAR_COUNTRY_ID_IRELAND = new Guid("35D72595-26D6-4167-B29D-F49282A40E20");
        public static readonly Guid MODULAR_COUNTRY_ID_ISLE_OF_MAN = new Guid("F79A50B3-D70E-47A1-B146-56A7D6471101");
        public static readonly Guid MODULAR_COUNTRY_ID_ISRAEL = new Guid("22B01FDF-EA60-461F-BC9F-BA21985DE7DB");
        public static readonly Guid MODULAR_COUNTRY_ID_ITALY = new Guid("BAA82FAA-8B2B-4F25-BA06-58468F0B2B9F");

        // J
        public static readonly Guid MODULAR_COUNTRY_ID_JAMAICA = new Guid("DAE65D82-F7B0-4B4F-987D-235ACEA43336");
        public static readonly Guid MODULAR_COUNTRY_ID_JAPAN = new Guid("00D41E48-0F46-42F6-8FCA-959881C8A583");
        public static readonly Guid MODULAR_COUNTRY_ID_JERSEY = new Guid("DF43E2F6-19FF-4751-BE16-BDC1536C2353");
        public static readonly Guid MODULAR_COUNTRY_ID_JORDAN = new Guid("0EA31740-8ED0-4252-B918-6BA0CA764101");

        // K
        public static readonly Guid MODULAR_COUNTRY_ID_KAZAKHSTAN = new Guid("4D25719B-AB4B-4F3D-B204-1BCACD587B81");
        public static readonly Guid MODULAR_COUNTRY_ID_KENYA = new Guid("9D4F206A-3F49-44FA-8EB0-FBE014024D95");
        public static readonly Guid MODULAR_COUNTRY_ID_KIRIBATI = new Guid("16FE8EE9-E566-47A7-82B2-5332079D2D13");
        public static readonly Guid MODULAR_COUNTRY_ID_KUWAIT = new Guid("7CC5BC8C-A02E-407B-B20C-2763C15E6F65");
        public static readonly Guid MODULAR_COUNTRY_ID_KYRGYZSTAN = new Guid("272D7A77-46A0-4AE6-BFF8-60C8179D2A99");

        // L
        public static readonly Guid MODULAR_COUNTRY_ID_LAOES = new Guid("6BAECA17-C607-413E-815E-C02D3F9A7AC5");
        public static readonly Guid MODULAR_COUNTRY_ID_LATVIA = new Guid("4270B3EF-BF63-4950-9F87-EB1E34BD91B1");
        public static readonly Guid MODULAR_COUNTRY_ID_LEBANON = new Guid("D2749A36-95C3-4A65-9DF2-12A8888CE5A7");
        public static readonly Guid MODULAR_COUNTRY_ID_LESOTHO = new Guid("BEE1D684-DCDF-402C-AFA0-DB5A1BCD962F");
        public static readonly Guid MODULAR_COUNTRY_ID_LIBERIA = new Guid("04ADD6F3-B889-4C5F-B5C2-715D053CDA92");
        public static readonly Guid MODULAR_COUNTRY_ID_LIBYA = new Guid("6A1E28AF-F088-4278-A906-0DF6414727AD");
        public static readonly Guid MODULAR_COUNTRY_ID_LIECHTENSTEIN = new Guid("FD2AF0CB-212A-4E33-9C32-8526DE6E68F4");
        public static readonly Guid MODULAR_COUNTRY_ID_LITHUANIA = new Guid("813C3DB4-A165-4952-9DCE-875031D0FDD9");
        public static readonly Guid MODULAR_COUNTRY_ID_LUXEMBOURG = new Guid("01CA8A41-16DE-4AA1-8479-5B18211F5145");

        // M
        public static readonly Guid MODULAR_COUNTRY_ID_MACEDONIA = new Guid("399E4E21-AA75-448A-948A-E24D68E35EAC");
        public static readonly Guid MODULAR_COUNTRY_ID_MADAGASCAR = new Guid("5BF4CB59-991A-40A8-94F6-3F03C6144463");
        public static readonly Guid MODULAR_COUNTRY_ID_MALAWI = new Guid("FB31E8BD-081D-460C-A495-E39B0ABF41A9");
        public static readonly Guid MODULAR_COUNTRY_ID_MALAYSIA = new Guid("15FE6CC6-B7BD-4B4C-98A2-84663B2085E6");
        public static readonly Guid MODULAR_COUNTRY_ID_MALDIVES = new Guid("EB173675-4FF5-4D10-84E8-B696749CF9E4");
        public static readonly Guid MODULAR_COUNTRY_ID_MALI = new Guid("4D0FD877-77DC-4C6A-9380-9D9E52142DDB");
        public static readonly Guid MODULAR_COUNTRY_ID_MALTA = new Guid("8D689B44-B0C2-4456-94A8-8946E409AD5D");
        public static readonly Guid MODULAR_COUNTRY_ID_MARSHALL_ISLANDS = new Guid("86BCBE1B-66FD-4941-A3F3-F1447FD5247A");
        public static readonly Guid MODULAR_COUNTRY_ID_MARTINIQUE = new Guid("282728E7-0351-42CD-AA23-83EA8EF9398B");
        public static readonly Guid MODULAR_COUNTRY_ID_MAURITANIA = new Guid("A0164BFF-0D57-4013-80D5-992FF22C14F3");
        public static readonly Guid MODULAR_COUNTRY_ID_MAURITIUS = new Guid("216A7C23-1970-46DA-B0BA-E25DB3EE37A6");
        public static readonly Guid MODULAR_COUNTRY_ID_MAYOTTE = new Guid("05EE4E33-AF93-4CDB-896F-3E7AD41491B0");
        public static readonly Guid MODULAR_COUNTRY_ID_MEXICO = new Guid("50056927-86A9-46F6-962D-F393227F9643");
        public static readonly Guid MODULAR_COUNTRY_ID_MICRONESIA = new Guid("B6BAA447-E746-4D43-A062-F6929F5F2CDC");
        public static readonly Guid MODULAR_COUNTRY_ID_MOLDOVA = new Guid("9F704CEF-858E-4AF9-8154-08061C122F8A");
        public static readonly Guid MODULAR_COUNTRY_ID_MONACO = new Guid("03E34F95-84FA-493E-A4C9-2E76AE7A4AF4");
        public static readonly Guid MODULAR_COUNTRY_ID_MONGOLIA = new Guid("95BFD03B-5BAA-48A8-B728-0CC97BECF1CB");
        public static readonly Guid MODULAR_COUNTRY_ID_MONTENEGRO = new Guid("3B526252-EB63-43DD-B5ED-20793D071FE8");
        public static readonly Guid MODULAR_COUNTRY_ID_MONTSERRAT = new Guid("A7C7A73E-1F6B-4CA7-AEA6-164063027E91");
        public static readonly Guid MODULAR_COUNTRY_ID_MOROCCO = new Guid("DF671088-A0AE-4804-B771-4325A7FE49AB");
        public static readonly Guid MODULAR_COUNTRY_ID_MOZAMBIQUE = new Guid("342BD51D-AD8C-4333-8642-4E81AB23A561");
        public static readonly Guid MODULAR_COUNTRY_ID_MYANMAR = new Guid("CDE6C663-F71C-4BC8-A2B7-D33D77128D79");

        // N
        public static readonly Guid MODULAR_COUNTRY_ID_NAMIBIA = new Guid("F64F78AD-2C26-456A-A318-C8F7DD5B4BD3");
        public static readonly Guid MODULAR_COUNTRY_ID_NAURU = new Guid("7205975E-E188-4D3C-9837-90CE2C8F207B");
        public static readonly Guid MODULAR_COUNTRY_ID_NEPAL = new Guid("7F4BBC62-E069-4D53-913E-3864B8474C77");
        public static readonly Guid MODULAR_COUNTRY_ID_NETHERLANDS = new Guid("CADE6738-1819-448D-9F4D-F43210A3C7F0");
        public static readonly Guid MODULAR_COUNTRY_ID_NETHERLANDS_ANTILLES = new Guid("85652759-4DBD-4C30-85F0-26CFB898B664");
        public static readonly Guid MODULAR_COUNTRY_ID_NEW_CALEDONIA = new Guid("B9B32FBC-1EA9-4F38-87C3-47B704E66114");
        public static readonly Guid MODULAR_COUNTRY_ID_NEW_ZEALAND = new Guid("41BEACD4-BA9C-43A8-94F5-1DAC05CA1437");
        public static readonly Guid MODULAR_COUNTRY_ID_NICARAGUA = new Guid("4D3F56DA-7C37-452D-8379-F296A9FA784F");
        public static readonly Guid MODULAR_COUNTRY_ID_NIGER = new Guid("DF0CEF3B-2131-4CFA-AC74-8B278D8D8D52");
        public static readonly Guid MODULAR_COUNTRY_ID_NIGERIA = new Guid("F2E7E21F-0262-44A1-9E6C-62C0F60175DE");
        public static readonly Guid MODULAR_COUNTRY_ID_NIUE = new Guid("34CB6270-2F8F-4369-9AA9-942818757DC6");
        public static readonly Guid MODULAR_COUNTRY_ID_NORFOLK_ISLAND = new Guid("9142746D-C27E-4BB1-9096-A313E1A79838");
        public static readonly Guid MODULAR_COUNTRY_ID_NORTH_KOREA = new Guid("47C306FF-445B-4E6C-973B-701653494375");
        public static readonly Guid MODULAR_COUNTRY_ID_NORTHERN_IRELAND = new Guid("D63644F6-4BB8-42C3-91D3-75D06F2DBA99");
        public static readonly Guid MODULAR_COUNTRY_ID_NORTHERN_MARIANA_ISLANDS = new Guid("34DF51DC-6925-4192-8FCB-B307050E1264");
        public static readonly Guid MODULAR_COUNTRY_ID_NORWAY = new Guid("267CC0A5-016F-4DEF-BFC3-88FE84D53C96");

        // O
        public static readonly Guid MODULAR_COUNTRY_ID_OMAN = new Guid("396A256A-375D-49BC-9769-0AD3E1B609D2");

        // P
        public static readonly Guid MODULAR_COUNTRY_ID_PAKISTAN = new Guid("4CC4AE45-D68F-4B88-B43B-9125A5A8B68B");
        public static readonly Guid MODULAR_COUNTRY_ID_PALAU = new Guid("2AE22715-4E93-4C53-91AA-CEBDA19A53E8");
        public static readonly Guid MODULAR_COUNTRY_ID_PALESTINE = new Guid("82C5D45C-C8DA-4CFC-A059-4D4DC9E60ED3");
        public static readonly Guid MODULAR_COUNTRY_ID_PANAMA = new Guid("C5494BBA-779D-4D01-B5DC-BE3E0AFE74FF");
        public static readonly Guid MODULAR_COUNTRY_ID_PAPUA_NEW_GUINEA = new Guid("E30B91E4-CD7A-4CCA-BA48-04D2AC539D1A");
        public static readonly Guid MODULAR_COUNTRY_ID_PARAGUAY = new Guid("36203914-E937-4550-833F-AC218407F996");
        public static readonly Guid MODULAR_COUNTRY_ID_PERU = new Guid("822E51A1-C820-413C-AAC0-3A4775EED825");
        public static readonly Guid MODULAR_COUNTRY_ID_PHILIPPINES = new Guid("CBCC49D8-C652-4332-993E-E9557C982A05");
        public static readonly Guid MODULAR_COUNTRY_ID_PITCAIRN = new Guid("10EFA3D5-E51C-498C-B823-E6F69BA48965");
        public static readonly Guid MODULAR_COUNTRY_ID_POLAND = new Guid("C50EC09E-395C-4E9B-B8AA-F04F7D1565AF");
        public static readonly Guid MODULAR_COUNTRY_ID_PORTUGAL = new Guid("3E314975-B0C2-4284-BAF1-86C89332AD5C");
        public static readonly Guid MODULAR_COUNTRY_ID_PUERTO_RICO = new Guid("8763C296-E38E-4524-A9F6-748840736586");

        // Q
        public static readonly Guid MODULAR_COUNTRY_ID_QATAR = new Guid("2C7FA61F-E5C0-4C56-8505-044CDCF9C015");

        // R
        public static readonly Guid MODULAR_COUNTRY_ID_REUNION = new Guid("A1AD7DA1-EF4A-490C-BB3B-452DB13D9EDB");
        public static readonly Guid MODULAR_COUNTRY_ID_ROMANIA = new Guid("F2DD4648-F9F1-4AD6-9F51-F9759D419681");
        public static readonly Guid MODULAR_COUNTRY_ID_RUSSIAN_FEDERATION = new Guid("C351BB60-6748-4461-97A7-AE1AEEB38084");
        public static readonly Guid MODULAR_COUNTRY_ID_RWANDA = new Guid("3C0789D9-6CB2-4D96-ACEE-725E2CE431A6");

        // S
        public static readonly Guid MODULAR_COUNTRY_ID_SAINT_HELENA = new Guid("A92D29D5-9281-4470-A2B8-2BEAEAC34675");
        public static readonly Guid MODULAR_COUNTRY_ID_SAINT_KITTS_AND_NEVIS = new Guid("F55B6311-3572-429B-BAAC-05B6FAA5D906");
        public static readonly Guid MODULAR_COUNTRY_ID_SAINT_LUCIA = new Guid("A7106489-81FA-422A-9D41-39C1053AF36A");
        public static readonly Guid MODULAR_COUNTRY_ID_SAINT_PIERRE_AND_MIQUELON = new Guid("65E2525B-BE3B-431A-9313-4CD2F6C399D6");
        public static readonly Guid MODULAR_COUNTRY_ID_SAINT_VINCENT_AND_THE_GRENADINES = new Guid("5F35337F-249E-4362-A8E8-EC02B88414B0");
        public static readonly Guid MODULAR_COUNTRY_ID_SAMOA = new Guid("81C76D52-0C33-41CA-9408-D3FA14B740CF");
        public static readonly Guid MODULAR_COUNTRY_ID_SAN_MARINO = new Guid("8B3463C9-1D1A-4939-9465-B559801857B9");
        public static readonly Guid MODULAR_COUNTRY_ID_SAO_TOME_AND_PRINCIPE = new Guid("A0683E63-7721-4EC9-BC89-6DFFFA87702E");
        public static readonly Guid MODULAR_COUNTRY_ID_SAUDI_ARABIA = new Guid("86E9DE37-61E0-421B-9B0C-AB1B3313BBBD");
        public static readonly Guid MODULAR_COUNTRY_ID_SENEGAL = new Guid("DE98C863-50F3-442C-8791-97645670DED5");
        public static readonly Guid MODULAR_COUNTRY_ID_SERBIA = new Guid("76D17243-FE45-4E23-A600-36E976078970");
        public static readonly Guid MODULAR_COUNTRY_ID_SEYCHELLES = new Guid("5C994EEF-87E9-41EC-A7C7-773320A3F9F5");
        public static readonly Guid MODULAR_COUNTRY_ID_SIERRA_LEONE = new Guid("B41DBFAF-1D5F-41CB-AB21-CCE78E37AC1A");
        public static readonly Guid MODULAR_COUNTRY_ID_SINGAPORE = new Guid("A8F813AC-E2D8-4228-94CD-1326DD7DE6C5");
        public static readonly Guid MODULAR_COUNTRY_ID_SLOVAKIA = new Guid("D167CA64-A173-49D9-8178-5F8266A73ACD");
        public static readonly Guid MODULAR_COUNTRY_ID_SLOVENIA = new Guid("1004ADDB-E539-48A8-A23F-47F1FF066E74");
        public static readonly Guid MODULAR_COUNTRY_ID_SOLOMON_ISLANDS = new Guid("70298C00-A2B1-4C95-A9B8-CB18123E4884");
        public static readonly Guid MODULAR_COUNTRY_ID_SOMALIA = new Guid("15632A10-56AE-4101-8020-D4245CA28295");
        public static readonly Guid MODULAR_COUNTRY_ID_SOUTH_AFRICA = new Guid("20F0B1C0-81C6-45F9-BDF2-C097DAF71C6F");
        public static readonly Guid MODULAR_COUNTRY_ID_SOUTH_GEORGIA_AND_THE_SOUTH_SANDWICH_ISLANDS = new Guid("B87CEA64-3FB6-40E7-89C8-3DA8C306F145");
        public static readonly Guid MODULAR_COUNTRY_ID_SOUTH_KOREA = new Guid("5DF0DCD4-07F1-4B27-A17A-2E9363734FCD");
        public static readonly Guid MODULAR_COUNTRY_ID_SOUTH_SUDAN = new Guid("3E06A05F-CFA3-4A41-AAD8-DFE3930B1D13");
        public static readonly Guid MODULAR_COUNTRY_ID_SPAIN = new Guid("FAC17AEE-134C-48E6-9A17-8C83311E525E");
        public static readonly Guid MODULAR_COUNTRY_ID_SRI_LANKA = new Guid("BD5811AE-B9CA-4569-9D17-7BF7BC782797");
        public static readonly Guid MODULAR_COUNTRY_ID_SUDAN = new Guid("8C78E6C3-1BF2-4872-A9FA-540893023A87");
        public static readonly Guid MODULAR_COUNTRY_ID_SURINAME = new Guid("6E62EA9B-5A64-4EB6-862D-8D035D3D6652");
        public static readonly Guid MODULAR_COUNTRY_ID_SVALBARD_AND_JAN_MAYEN = new Guid("5DF185BD-715A-44B1-A921-DE1DF8EBB4BA");
        public static readonly Guid MODULAR_COUNTRY_ID_SWAZILAND = new Guid("7919770A-C850-45FF-AF46-4DA089D09F5D");
        public static readonly Guid MODULAR_COUNTRY_ID_SWEDEN = new Guid("3380C85F-1A8F-4129-9C6E-785F2A91F991");
        public static readonly Guid MODULAR_COUNTRY_ID_SWITZERLAND = new Guid("BD6BAC06-C308-4676-9E41-BE843DD5F9F9");
        public static readonly Guid MODULAR_COUNTRY_ID_SYRIA = new Guid("D8C53767-7CB9-4E36-BF9F-DA82D370D7EB");

        // T
        public static readonly Guid MODULAR_COUNTRY_ID_TAIWAN = new Guid("497AB1B7-8AF8-4E46-8BAC-75CBCC984FD1");
        public static readonly Guid MODULAR_COUNTRY_ID_TAJIKISTAN = new Guid("4CDBA3ED-2F9E-4043-B5A4-B97FD261FB55");
        public static readonly Guid MODULAR_COUNTRY_ID_TANZANIA = new Guid("537EC8F7-F2BD-48FA-B1CF-3DA771D4E3F7");
        public static readonly Guid MODULAR_COUNTRY_ID_THAILAND = new Guid("EA6C3373-2CDD-4F12-8A83-6DF989E989A0");
        public static readonly Guid MODULAR_COUNTRY_ID_TIMOR_LESTE = new Guid("BF0986CC-92F7-4D21-A824-4C003EA64641");
        public static readonly Guid MODULAR_COUNTRY_ID_TOGO = new Guid("D75A85D0-F98C-47E0-BC22-E4F73746230A");
        public static readonly Guid MODULAR_COUNTRY_ID_TOKELAU = new Guid("3BFF1232-8FCA-4685-8592-755634F499EB");
        public static readonly Guid MODULAR_COUNTRY_ID_TONGA = new Guid("FF992051-82E2-4FA8-A897-B3F7C7E836D5");
        public static readonly Guid MODULAR_COUNTRY_ID_TRINIDAD_AND_TOBAGO = new Guid("C4D678DF-253C-456B-A992-E94E218C1BAE");
        public static readonly Guid MODULAR_COUNTRY_ID_TUNISIA = new Guid("97CDFD0F-8443-4F92-B654-0883982434C3");
        public static readonly Guid MODULAR_COUNTRY_ID_TURKEY = new Guid("EACF5268-1276-4CE6-84D0-62730021283F");
        public static readonly Guid MODULAR_COUNTRY_ID_TURKMENISTAN = new Guid("100D84FC-16B5-4176-82DA-B9321DD374CF");
        public static readonly Guid MODULAR_COUNTRY_ID_TURKS_AND_CAICOS_ISLANDS = new Guid("A573D714-FA9D-49EE-9E97-C94666B4E77E");
        public static readonly Guid MODULAR_COUNTRY_ID_TUVALU = new Guid("269AB962-6D49-4E55-9743-4C3C4A7298E1");

        // U
        public static readonly Guid MODULAR_COUNTRY_ID_UGANDA = new Guid("84F66F42-125E-405D-94CB-D0E5FB57A6E9");
        public static readonly Guid MODULAR_COUNTRY_ID_UKRAINE = new Guid("FE864AAD-1BD7-4C73-B115-9873004AF054");
        public static readonly Guid MODULAR_COUNTRY_ID_UNITED_ARAB_EMIRATES = new Guid("8A538B1D-030F-4C91-B34E-36B4C4D622FC");
        public static readonly Guid MODULAR_COUNTRY_ID_UNITED_KINGDOM = new Guid("28963D5C-C996-49D6-AADF-77371274C5E5");
        public static readonly Guid MODULAR_COUNTRY_ID_UNITED_STATES = new Guid("89E3D0A4-0786-4274-BFC1-8D6AC26896DA");
        public static readonly Guid MODULAR_COUNTRY_ID_UNITED_STATES_MINOR_OUTLYING_ISLANDS = new Guid("B46B6B1F-0F99-4921-8FDC-77FAB71552C8");
        public static readonly Guid MODULAR_COUNTRY_ID_URUGUAY = new Guid("682D10A4-8B37-4C6F-95C7-13392C8345E1");
        public static readonly Guid MODULAR_COUNTRY_ID_UZBEKISTAN = new Guid("D10A5E9D-F20B-4717-92EE-B24721BBCFFC");

        // V
        public static readonly Guid MODULAR_COUNTRY_ID_VANUATU = new Guid("84FA0371-0884-4E9A-BB30-D16985F4E4AD");
        public static readonly Guid MODULAR_COUNTRY_ID_VATICAN_CITY = new Guid("B4ED3C9E-4582-49B4-A972-8954C9C4C533");
        public static readonly Guid MODULAR_COUNTRY_ID_VENEZUELA = new Guid("D77BAB3F-A304-415F-82BE-A055FA6BBC7D");
        public static readonly Guid MODULAR_COUNTRY_ID_VIETNAM = new Guid("F15FADEF-16A9-4638-8FB4-D2F010946E1B");
        public static readonly Guid MODULAR_COUNTRY_ID_VIRGIN_ISLANDS_BRITISH = new Guid("A7CA25AF-F55A-4345-AF39-695A14B80F16");
        public static readonly Guid MODULAR_COUNTRY_ID_VIRGIN_ISLANDS_US = new Guid("CA8D87BF-0F97-47B0-ABEE-7912DC559D26");

        // W
        public static readonly Guid MODULAR_COUNTRY_ID_WALLIS_AND_FUTUNA = new Guid("F4DF1246-1164-4EA4-809F-EF55E05A8AA7");
        public static readonly Guid MODULAR_COUNTRY_ID_WESTERN_SAHARA = new Guid("3ACA28C5-2273-403F-B2A6-DCC1ED741966");

        // Y
        public static readonly Guid MODULAR_COUNTRY_ID_YEMEN = new Guid("9219E562-FCD2-47E0-AC89-F834AC09C370");

        // Z
        public static readonly Guid MODULAR_COUNTRY_ID_ZAMBIA = new Guid("FAE501DF-4C8A-46CF-B0C7-51B81464F877");
        public static readonly Guid MODULAR_COUNTRY_ID_ZIMBABWE = new Guid("C1487AC1-AF80-4FA0-BA07-D835D8D32753");


        #endregion

        #region "  Constructors  "

        public Country()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "";

        #endregion

        #region "  Properties  "

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        #endregion

        #region "  Static Methods  "

        public static new List<Country> LoadInstances()
        {
            return FetchAll();
        }

        public static new Country Load(Guid ID)
        {
            Country obj = new Country();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region "  Data Methods  "

        protected static List<Country> FetchAll()
        {
            List<Country> AllObjects = new List<Country>();

            if (Database.CheckDatabaseConnection())
            {
                Database.DatabaseConnectivityMode DatabaseConnectionMode = Database.ConnectionMode;
                PropertyInfo[] AllProperties = GetProperties();
                if (AllProperties != null)
                {
                    switch (DatabaseConnectionMode)
                    {

                        case Database.DatabaseConnectivityMode.Remote:
                            using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                            {
                                Connection.Open();

                                if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                                {
                                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                                }

                                using (SqlCommand Command = new SqlCommand())
                                {
                                    Command.Connection = Connection;
                                    Command.CommandType = CommandType.Text;
                                    Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                    using (SqlDataReader DataReader = Command.ExecuteReader())
                                    {
                                        Country obj = GetOrdinals(DataReader);

                                        while (DataReader.Read())
                                        {
                                            AllObjects.Add(obj);
                                        }
                                    }
                                }

                                Connection.Close();
                            }
                            break;

                        case Database.DatabaseConnectivityMode.Local:
                            using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                            {
                                Connection.Open();

                                if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                                {
                                    using (SqliteCommand Command = new SqliteCommand())
                                    {

                                        Command.Connection = Connection;
                                        Command.CommandType = CommandType.Text;
                                        Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                        using (SqliteDataReader DataReader = Command.ExecuteReader())
                                        {
                                            Country obj = GetOrdinals(DataReader);

                                            while (DataReader.Read())
                                            {
                                                AllObjects.Add(obj);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                                }
                                Connection.Close();
                            }
                            break;
                    }
                }

                return AllObjects;

            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "Database connection error.");
            }
        }

        protected static Country GetOrdinals(SqlDataReader DataReader)
        {
            Country obj = new Country();

            PropertyInfo[] AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        protected static Country GetOrdinals(SqliteDataReader DataReader)
        {
            Country obj = new Country();

            PropertyInfo[] AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        #endregion

    }
}