using MPI_Thu_Thap.DAO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPI_Thu_Thap.BUS
{
    public class getDataFirstPage
    {
        public IWebDriver driver;
        public void download()
        {
            string Url = "https://muasamcong.mpi.gov.vn/web/guest/contractor-selection?render=index";
            ChromeDriverService hideConsole = ChromeDriverService.CreateDefaultService();
            hideConsole.HideCommandPromptWindow = true;
            driver = new ChromeDriver(hideConsole);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Url);
            while (true)
            {
                checkAndClick();
            }

        }
        void checkAndClick()
        {
            string main_XPath = "", maTBMT_Xpath = "", thoiGianDongThau_Xpath = "";
            int i = 0/*, j = 1*/;
            string maTBMT = "", thoiGianDongThau = "";
            DateTime timer = DateTime.Now;
            for (i = 10; i >= 1; i--)
            {
                main_XPath = "//*[@id=\"bid-closed\"]/div[" + i + "]/div/div[2]/div[1]/a/h5";
                maTBMT_Xpath = "//*[@id=\"bid-closed\"]/div[" + i + "]/div/div[1]/p";
                thoiGianDongThau_Xpath = "//*[@id=\"bid-closed\"]/div[" + i + "]/div/div[2]/div[2]/div/h5[2]";
                IWebElement elementMain = driver.FindElement(By.XPath(main_XPath));
                IWebElement elementTBMT = driver.FindElement(By.XPath(maTBMT_Xpath)); // Fixed XPath
                IWebElement elementThoiGianDongThau = driver.FindElement(By.XPath(thoiGianDongThau_Xpath)); // Fixed XPath
                thoiGianDongThau = elementThoiGianDongThau.Text;
                DateTime thoiGianDongThauDateTime = DateTime.ParseExact(thoiGianDongThau, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (thoiGianDongThauDateTime <= timer)
                {
                    driver.Quit(); // Tắt trình duyệt Chrome
                    driver.Dispose(); // Giải phóng tài nguyên của trình duyệt Chrome
                    i = 0;
                    Thread.Sleep(300000);
                    break;
                }
                else
                {
                    if (i == 1)
                    {
                        driver.Navigate().Refresh();
                        i = 10;                        
                    }
                    //Thêm cơ chế check mã thông báo mời thầu trước khi ấn xem mã này đã tồn tại trong database chưa.
                    bool check;
                    string xpathCheck = "//*[@id=\"bid-closed\"]/div[" + i + "]/div/div[1]/p";
                    string matbmtCheck = "";
                    IWebElement xpathCheckElement = driver.FindElement(By.XPath(xpathCheck));
                    matbmtCheck = xpathCheckElement.Text;
                    matbmtCheck = matbmtCheck.Substring("Mã TBMT: ".Length, "IB2300108379-02".Length);
                    checkTBMT checkTBMT = new checkTBMT();
                    check = checkTBMT.checkTB(matbmtCheck);
                    if (check == true)
                    {
                        continue;
                    }
                    else
                    {
                        //Thực hiện click vào mục tiêu của main_Xpath
                        elementMain.Click();
                        Thread.Sleep(15000);
                        //getDataFromXpath();
                        getData1();
                        driver.Navigate().Back();
                        Thread.Sleep(7000);
                    }
                }

            }
        }

        void getData1()
        {
            string conStr = "Data Source=DESKTOP-ASUUQ5M;Initial Catalog=dataMPI;Integrated Security=True";
            SqlConnection conn = new SqlConnection(conStr);
            string check = "";
            thongTinCoBan ttcb = new thongTinCoBan();
            thongTinGoiThau ttgt = new thongTinGoiThau();
            thongTinChungKHLCNT ttckhlcnt = new thongTinChungKHLCNT();
            cachThucDuThau ctdt = new cachThucDuThau();
            thongTinDuThau ttdt = new thongTinDuThau();
            thongTinQuyetDinhPheDuyet ttqdpd = new thongTinQuyetDinhPheDuyet();

            try
            {
                string xpathCheck = "//*[@id=\"info-general\"]/div[4]/div[2]/div[2]/div[1]";
                IWebElement element = driver.FindElement(By.XPath(xpathCheck));
                check = element.Text;

                //list xpath thông tin cơ bản
                List<string> xpath_ttcb = ttcb.getData();
                IWebElement elementTbmt = driver.FindElement(By.XPath(xpath_ttcb[0]));
                IWebElement elementNgayDangTai = driver.FindElement(By.XPath(xpath_ttcb[1]));
                IWebElement elementPhienBanThayDoi = driver.FindElement(By.XPath(xpath_ttcb[2]));
                //Khai báo biến để lưu thông tin cơ bản
                string maTBMT = elementTbmt.Text;
                //DateTime ngayDangTai = DateTime.ParseExact(elementNgayDangTai.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string ngayDangTai = elementNgayDangTai.Text;
                string phienBanThayDoi = elementPhienBanThayDoi.Text;

                //list xpath thông tin chung KHLCNT
                List<string> xpath_ttckhlcnt = ttckhlcnt.getData();
                IWebElement elementkhlcnt = driver.FindElement(By.XPath(xpath_ttckhlcnt[0]));
                IWebElement elementPhanLoaiKHLCNT = driver.FindElement(By.XPath(xpath_ttckhlcnt[1]));
                IWebElement elementTenDuToanMuaSam = driver.FindElement(By.XPath(xpath_ttckhlcnt[2]));
                //Khai báo biến để lưu thông tin chung
                string maKHLCNT = elementkhlcnt.Text;
                string PhanLoaiKHLCNT = elementPhanLoaiKHLCNT.Text;
                string TenDuToanMuaSam = elementTenDuToanMuaSam.Text;

                //List thông tin gói thầu
                string TenGoiThau;
                string ChuDauTu = "";
                string BenMoiThau;
                string NguonVon;
                string LinhVuc;
                string HinhThucLuaChonNhaThau;
                string LoaiHopDong;
                string TrongNuocQuocTe;
                string PhuongThucLuaChonNhaThau;
                string ThoiGianThucHienHD;

                List<string> xpath_ttgt = ttgt.getData(check);
                if (check == "Chủ đầu tư")
                {
                    IWebElement elementTenGoiThau = driver.FindElement(By.XPath(xpath_ttgt[0]));
                    IWebElement elementChuDauTu = driver.FindElement(By.XPath(xpath_ttgt[1]));
                    IWebElement elementBenMoiThau = driver.FindElement(By.XPath(xpath_ttgt[2]));
                    IWebElement elementNguonVon = driver.FindElement(By.XPath(xpath_ttgt[3]));
                    IWebElement elementLinhVuc = driver.FindElement(By.XPath(xpath_ttgt[4]));
                    IWebElement elementHinhThucLuaChonNhaThau = driver.FindElement(By.XPath(xpath_ttgt[5]));
                    IWebElement elementLoaiHopDong = driver.FindElement(By.XPath(xpath_ttgt[6]));
                    IWebElement elementTrongNuocQuocTe = driver.FindElement(By.XPath(xpath_ttgt[7]));
                    IWebElement elementPhuongThucLuaChonNhaThau = driver.FindElement(By.XPath(xpath_ttgt[8]));
                    IWebElement elementThoiGianThucHienHD = driver.FindElement(By.XPath(xpath_ttgt[9]));

                    TenGoiThau = elementTenGoiThau.Text;
                    ChuDauTu = elementChuDauTu.Text;
                    BenMoiThau = elementBenMoiThau.Text;
                    NguonVon = elementNguonVon.Text;
                    LinhVuc = elementLinhVuc.Text;
                    HinhThucLuaChonNhaThau = elementHinhThucLuaChonNhaThau.Text;
                    LoaiHopDong = elementLoaiHopDong.Text;
                    TrongNuocQuocTe = elementTrongNuocQuocTe.Text;
                    PhuongThucLuaChonNhaThau = elementPhuongThucLuaChonNhaThau.Text;
                    ThoiGianThucHienHD = elementThoiGianThucHienHD.Text;
                }
                else
                {
                    IWebElement elementTenGoiThau = driver.FindElement(By.XPath(xpath_ttgt[0]));
                    IWebElement elementBenMoiThau = driver.FindElement(By.XPath(xpath_ttgt[1]));
                    IWebElement elementNguonVon = driver.FindElement(By.XPath(xpath_ttgt[2]));
                    IWebElement elementLinhVuc = driver.FindElement(By.XPath(xpath_ttgt[3]));
                    IWebElement elementHinhThucLuaChonNhaThau = driver.FindElement(By.XPath(xpath_ttgt[4]));
                    IWebElement elementLoaiHopDong = driver.FindElement(By.XPath(xpath_ttgt[5]));
                    IWebElement elementTrongNuocQuocTe = driver.FindElement(By.XPath(xpath_ttgt[6]));
                    IWebElement elementPhuongThucLuaChonNhaThau = driver.FindElement(By.XPath(xpath_ttgt[7]));
                    IWebElement elementThoiGianThucHienHD = driver.FindElement(By.XPath(xpath_ttgt[8]));

                    TenGoiThau = elementTenGoiThau.Text;
                    BenMoiThau = elementBenMoiThau.Text;
                    NguonVon = elementNguonVon.Text;
                    LinhVuc = elementLinhVuc.Text;
                    HinhThucLuaChonNhaThau = elementHinhThucLuaChonNhaThau.Text;
                    LoaiHopDong = elementLoaiHopDong.Text;
                    TrongNuocQuocTe = elementTrongNuocQuocTe.Text;
                    PhuongThucLuaChonNhaThau = elementPhuongThucLuaChonNhaThau.Text;
                    ThoiGianThucHienHD = elementThoiGianThucHienHD.Text;
                }



                //list xpath cách thức dự thầu
                List<string> xpath_ctdtt = ctdt.getData();
                IWebElement elementHinhThucDuThau = driver.FindElement(By.XPath(xpath_ctdtt[0]));
                IWebElement elementDiaDiemPhatHanhEHSMT = driver.FindElement(By.XPath(xpath_ctdtt[1]));
                IWebElement elementChiPhiNopEHSDT = driver.FindElement(By.XPath(xpath_ctdtt[2]));
                IWebElement elementDiaDiemNhanEHSDT = driver.FindElement(By.XPath(xpath_ctdtt[3]));
                IWebElement elementDiaDiemThucHienGoiThau = driver.FindElement(By.XPath(xpath_ctdtt[4]));
                //Khai báo biến để lưu cách thức dự thầu
                string HinhThucDuThau = elementHinhThucDuThau.Text;
                string DiaDiemPhatHanhEHSMT = elementDiaDiemPhatHanhEHSMT.Text;
                string ChiPhiNopEHSDT = elementChiPhiNopEHSDT.Text;
                string DiaDiemNhanEHSDT = elementDiaDiemNhanEHSDT.Text;
                string DiaDiemThucHienGoiThau = elementDiaDiemThucHienGoiThau.Text;


                //list xpath thông tin dự thầu
                List<string> xpath_ttdt = ttdt.getData();
                IWebElement elementThoiDiemDongThau = driver.FindElement(By.XPath(xpath_ttdt[0]));
                IWebElement elementThoiDiemMoThau = driver.FindElement(By.XPath(xpath_ttdt[1]));
                IWebElement elementDiaDiemMoThau = driver.FindElement(By.XPath(xpath_ttdt[2]));
                IWebElement elementHieuLucBaoGia = driver.FindElement(By.XPath(xpath_ttdt[3]));
                //Khai báo biến để lưu thông tin cơ bản
                //DateTime ThoiDiemDongThau = DateTime.ParseExact(elementThoiDiemDongThau.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string ThoiDiemDongThau = elementThoiDiemDongThau.Text;
                //DateTime ThoiDiemMoThau = DateTime.ParseExact(elementThoiDiemMoThau.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string ThoiDiemMoThau = elementThoiDiemMoThau.Text;
                string DiaDiemMoThau = elementDiaDiemMoThau.Text;
                string HieuLucBaoGia = elementHieuLucBaoGia.Text;

                //list xpath thông tin quyết định phê duyệt
                List<string> xpath_ttqdpd = ttqdpd.getData();
                IWebElement elementSoQuyetDinhPheDuyet = driver.FindElement(By.XPath(xpath_ttqdpd[0]));
                IWebElement elementNgayPheDuyet = driver.FindElement(By.XPath(xpath_ttqdpd[1]));
                IWebElement elementCoQuanBanHanhQuyetDinh = driver.FindElement(By.XPath(xpath_ttqdpd[2]));
                IWebElement elementQuyetDinhPheDuyet = driver.FindElement(By.XPath(xpath_ttqdpd[3]));
                //Khai báo biến để lưu thông tin quyết định phê duyệt      
                string SoQuyetDinhPheDuyet = elementSoQuyetDinhPheDuyet.Text;
                //DateTime NgayPheDuyet = DateTime.ParseExact(elementNgayPheDuyet.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string NgayPheDuyet = elementNgayPheDuyet.Text;
                string CoQuanBanHanhQuyetDinh = elementCoQuanBanHanhQuyetDinh.Text;
                string QuyetDinhPheDuyet = elementQuyetDinhPheDuyet.Text;



                //[NgayPheDuyet] - [ThoiDiemMoThau] - [ThoiDiemDongThau] - [NgayDangTai]
                //thêm vào sql
                //string query = "insert into TBMT values('" + maTBMT + "', '" + ngayDangTai + "', '" + phienBanThayDoi + "')";
                string qr = "";
                if (check == "Chủ đầu tư")
                {
                    qr = "INSERT INTO TBMT VALUES(N'" + maTBMT + "', '" + ngayDangTai + "',N'" + phienBanThayDoi + "',N'" + maKHLCNT + "', N'" + PhanLoaiKHLCNT + "',N'" + TenDuToanMuaSam + "',N'" + TenGoiThau + "',N'" + ChuDauTu + "',N'" + BenMoiThau + "',N'" + NguonVon + "',N'" + LinhVuc + "',N'" + HinhThucLuaChonNhaThau + "',N'" + LoaiHopDong + "',N'" + TrongNuocQuocTe + "',N'" + PhuongThucLuaChonNhaThau + "',N'" + ThoiGianThucHienHD + "',N'" + HinhThucDuThau + "',N'" + DiaDiemPhatHanhEHSMT + "',N'" + ChiPhiNopEHSDT + "',N'" + DiaDiemNhanEHSDT + "',N'" + DiaDiemThucHienGoiThau + "','" + ThoiDiemDongThau + "','" + ThoiDiemMoThau + "',N'" + DiaDiemMoThau + "',N'" + HieuLucBaoGia + "', N'" + SoQuyetDinhPheDuyet + "','" + NgayPheDuyet + "', N'" + CoQuanBanHanhQuyetDinh + "', N'" + QuyetDinhPheDuyet + "')";

                }
                else
                {
                    qr = "INSERT INTO TBMT VALUES(N'" + maTBMT + "', '" + ngayDangTai + "',N'" + phienBanThayDoi + "',N'" + maKHLCNT + "', N'" + PhanLoaiKHLCNT + "',N'" + TenDuToanMuaSam + "',N'" + TenGoiThau + "',N'" + ChuDauTu + "',N'" + BenMoiThau + "',N'" + NguonVon + "',N'" + LinhVuc + "',N'" + HinhThucLuaChonNhaThau + "',N'" + LoaiHopDong + "',N'" + TrongNuocQuocTe + "',N'" + PhuongThucLuaChonNhaThau + "',N'" + ThoiGianThucHienHD + "',N'" + HinhThucDuThau + "',N'" + DiaDiemPhatHanhEHSMT + "',N'" + ChiPhiNopEHSDT + "',N'" + DiaDiemNhanEHSDT + "',N'" + DiaDiemThucHienGoiThau + "','" + ThoiDiemDongThau + "','" + ThoiDiemMoThau + "',N'" + DiaDiemMoThau + "',N'" + HieuLucBaoGia + "', N'" + SoQuyetDinhPheDuyet + "','" + NgayPheDuyet + "', N'" + CoQuanBanHanhQuyetDinh + "', N'" + QuyetDinhPheDuyet + "')";
                }

                conn.Open();
                SqlCommand cmd = new SqlCommand(qr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
