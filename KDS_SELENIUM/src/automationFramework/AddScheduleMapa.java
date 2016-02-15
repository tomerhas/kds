package automationFramework;

import java.sql.SQLException;

import JDBC.DB_DML;

import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import pageObjects.HosafatSidur;
import pageObjects.WorkCard;
import utils.Base;
import utils.Utils;




@Listeners ({Listener.TestListener.class})
public class AddScheduleMapa    extends Base  {
	
	
	public WebDriver driver ;
	
	
	
	
	
	
  @Test
  public void addScheduleMapa () throws SQLException {
	  DB_DML.deleteRecordFromTable("77104", "to_date('23/06/2015','dd/mm/yyyy')", "33011");
	  Utils a= new Utils();
	  a.waitForWindow("WorkCard",driver);
	  WorkCard.TxtId(driver).sendKeys("77104");
	  WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("24062015");
	  WorkCard.Btn_Show(driver).click();
	  WorkCard.Wait_For_Element_Stalenes(driver, "btnFindSidur",null);
	  WorkCard.Btn_Add_Mapa(driver).click();
	  Utils b= new Utils();
	  b.waitForWindow("HosafatSidur",driver);
	  HosafatSidur.Txt_Sidur_Mapa(driver).sendKeys("99999");
	  HosafatSidur.Btn_Show_Mapa(driver).click();
	  Assert.assertEquals(HosafatSidur.Validate_Popup(driver).getText(),"יש להזין מספר סידורי שאינו מתחיל בספרות 99");
	  HosafatSidur.Txt_Sidur_Mapa(driver).clear();
	  //Hosafat_Sidur.Txt_Sidur_Mapa(driver).sendKeys("33011");
	  //Hosafat_Sidur.Btn_Show_Mapa(driver).click();
	  //Assert.assertEquals(Hosafat_Sidur.Validate_Popup(driver).getText(),"כרטיס ללא התייחסות, לא ניתן להוסיף סידור זה");
	  //Hosafat_Sidur.Txt_Sidur_Mapa(driver).clear();
	  HosafatSidur.Txt_Sidur_Mapa(driver).sendKeys("58011");
	  HosafatSidur.Btn_Show_Mapa(driver).click();
	  HosafatSidur.Btn_Cheak_ALL(driver).click();
	  Assert.assertTrue( HosafatSidur.Chk_Activity_1(driver).isSelected(), "the cheak-box is not selected" );
	  Assert.assertTrue( HosafatSidur.Chk_Activity_2(driver).isSelected(), "the cheak-box is not selected" );
	  HosafatSidur.Btn_Clear_ALL(driver).click();
	  Assert.assertFalse( HosafatSidur.Chk_Activity_1(driver).isSelected(), "the cheak-box is selected" );
	  Assert.assertFalse( HosafatSidur.Chk_Activity_2(driver).isSelected(), "the cheak-box is selected" );
	  HosafatSidur.Btn_Cheak_ALL(driver).click();
	  Assert.assertFalse( HosafatSidur.Car_No_Mapa_1(driver).isEnabled(), "Car_No_Mapa is enebled" );
	  HosafatSidur.Car_No_Mapa_2(driver).click();
	  HosafatSidur.Car_No_Mapa_2(driver).sendKeys("34918");
	  HosafatSidur.Btn_Update_Mapa(driver).click();
	  Utils c= new Utils();
	  c.waitForWindow("WorkCard",driver);
	  WorkCard.Wait_For_Element_Visibile(driver,60, "btnRefreshOvedDetails");
	  WorkCard.Btn_Show(driver).click();
	  //Work_Card.Wait_For_Element_Visibile(driver, 60, "SD_imgCancel0");
	  Assert.assertEquals(WorkCard.Lbl_Sidur_Num_0(driver).getText(), "58011");
	  WorkCard.Cancel_Sidur(driver).click();
	  WorkCard.Btn_Update(driver).click();
	  WorkCard.Wait_For_Element_Stalenes(driver, "clnDate",null);
	  WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("23062015");
	  WorkCard.Btn_Show(driver).click();
	  WorkCard.Wait_For_Element_Stalenes(driver, "btnFindSidur",null);
	  WorkCard.Btn_Add_Mapa(driver).click();
	  Utils d= new Utils();
	  d.waitForWindow("HosafatSidur",driver);
	  HosafatSidur.Txt_Sidur_Mapa(driver).sendKeys("33011");
	  HosafatSidur.Btn_Show_Mapa(driver).click();
	  HosafatSidur.Btn_Cheak_ALL(driver).click();
	  HosafatSidur.Car_No_Mapa_2(driver).click();
	  HosafatSidur.Car_No_Mapa_2(driver).sendKeys("34918");
	  HosafatSidur.Btn_Update_Mapa(driver).click();
	  WorkCard.Btn_Yes_Copy_Car_Num(driver).click();
	  HosafatSidur.Btn_Update_Mapa(driver).click();
	  Utils e= new Utils();
	  e.waitForWindow("WorkCard",driver);
	  //c.waitForWindow("WorkCard",driver);
	  //Work_Card.Wait_For_Element_Visibile(driver,60, "btnRefreshOvedDetails");
	  //Work_Card.Wait_For_Element_Visibile(driver, 80, "SD_imgCancel2");
	  //Work_Card.Wait_For_Element_Stalenes(driver, "btnRefreshOvedDetails");
	  //Work_Card.Btn_Show(driver).click();
	  Assert.assertEquals(WorkCard.Lbl_Sidur_No_2(driver).getText(), "33011");
	  //DB_DML.deleteRecordFromTable("77104", "to_date('23/06/2015','dd/mm/yyyy')", "33011");
	  //Work_Card.Cancel_Schedule_02(driver).click();
	  //Work_Card.Wait_For_Element_Visibile(driver, 60, "btnUpdateCard");
	  //Work_Card.Wait_For_Element_Stalenes(driver, "btnUpdateCard");
	  //Work_Card.Btn_Update(driver).click();
	  //Work_Card.Wait_For_Element_Stalenes(driver,"btnCloseCard",null);
	  WorkCard.Btn_Close(driver).click();
	  
	  
	  
	  
  }
  
  
  
  
  

  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=getDriver();
		//  driver=Base.Initialize_browser();
		//  Base.Initialize_Webpage(driver);
		Utils.Enter_Workcard(driver);
	  
	  
	  
  }

}
