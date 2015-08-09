package automationFramework;

import org.openqa.selenium.Alert;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;

import pageObjects.Hosafat_Sidur;
import pageObjects.Work_Card;
import utils.Base;
import utils.Utils;




@Listeners ({Listener.TestListener.class})
public class Test_Add_Schedule_Mapa    extends Base  {
	
	
	public WebDriver driver ;
	
	
	
	
	
	
  @Test
  public void Add_Schedule_Mapa () {
	  
	  Utils a= new Utils();
	  a.waitForWindow("WorkCard",driver);
	  Work_Card.TxtId(driver).sendKeys("77104");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("24062015");
	  Work_Card.Btn_Show(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver, "btnFindSidur");
	  Work_Card.Btn_Add_Mapa(driver).click();
	  Utils b= new Utils();
	  b.waitForWindow("HosafatSidur",driver);
	  Hosafat_Sidur.Txt_Sidur_Mapa(driver).sendKeys("99999");
	  Hosafat_Sidur.Btn_Show_Mapa(driver).click();
	  Assert.assertEquals(Hosafat_Sidur.Validate_Popup(driver).getText(),"יש להזין מספר סידורי שאינו מתחיל בספרות 99");
	  Hosafat_Sidur.Txt_Sidur_Mapa(driver).clear();
	  //Hosafat_Sidur.Txt_Sidur_Mapa(driver).sendKeys("33011");
	  //Hosafat_Sidur.Btn_Show_Mapa(driver).click();
	  //Assert.assertEquals(Hosafat_Sidur.Validate_Popup(driver).getText(),"כרטיס ללא התייחסות, לא ניתן להוסיף סידור זה");
	  //Hosafat_Sidur.Txt_Sidur_Mapa(driver).clear();
	  Hosafat_Sidur.Txt_Sidur_Mapa(driver).sendKeys("58011");
	  Hosafat_Sidur.Btn_Show_Mapa(driver).click();
	  Hosafat_Sidur.Btn_Cheak_ALL(driver).click();
	  Assert.assertTrue( Hosafat_Sidur.Chk_Activity_1(driver).isSelected(), "the cheak-box is not selected" );
	  Assert.assertTrue( Hosafat_Sidur.Chk_Activity_2(driver).isSelected(), "the cheak-box is not selected" );
	  Hosafat_Sidur.Btn_Clear_ALL(driver).click();
	  Assert.assertFalse( Hosafat_Sidur.Chk_Activity_1(driver).isSelected(), "the cheak-box is selected" );
	  Assert.assertFalse( Hosafat_Sidur.Chk_Activity_2(driver).isSelected(), "the cheak-box is selected" );
	  Hosafat_Sidur.Btn_Cheak_ALL(driver).click();
	  Assert.assertFalse( Hosafat_Sidur.Car_No_Mapa_1(driver).isEnabled(), "Car_No_Mapa is enebled" );
	  Hosafat_Sidur.Car_No_Mapa_2(driver).click();
	  Hosafat_Sidur.Car_No_Mapa_2(driver).sendKeys("34918");
	  Hosafat_Sidur.Btn_Update_Mapa(driver).click();
	  Utils c= new Utils();
	  c.waitForWindow("WorkCard",driver);
	  Work_Card.Wait_For_Element_Visibile(driver,60, "btnRefreshOvedDetails");
	  Work_Card.Btn_Show(driver).click();
	  //Work_Card.Wait_For_Element_Visibile(driver, 60, "SD_imgCancel0");
	  Assert.assertEquals(Work_Card.Sidur_Num(driver).getText(), "58011");
	  Work_Card.Cancel_Sidur(driver).click();
	  Work_Card.Btn_Update(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver, "clnDate");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("23062015");
	  Work_Card.Btn_Show(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver, "btnFindSidur");
	  Work_Card.Btn_Add_Mapa(driver).click();
	  Utils d= new Utils();
	  d.waitForWindow("HosafatSidur",driver);
	  Hosafat_Sidur.Txt_Sidur_Mapa(driver).sendKeys("33011");
	  Hosafat_Sidur.Btn_Show_Mapa(driver).click();
	  Hosafat_Sidur.Btn_Cheak_ALL(driver).click();
	  Hosafat_Sidur.Car_No_Mapa_2(driver).click();
	  Hosafat_Sidur.Car_No_Mapa_2(driver).sendKeys("34918");
	  Hosafat_Sidur.Btn_Update_Mapa(driver).click();
	  Work_Card.Btn_Yes_Copy_Car_Num(driver).click();
	  Hosafat_Sidur.Btn_Update_Mapa(driver).click();
	  Utils e= new Utils();
	  e.waitForWindow("WorkCard",driver);
	  //c.waitForWindow("WorkCard",driver);
	  //Work_Card.Wait_For_Element_Visibile(driver,60, "btnRefreshOvedDetails");
	  //Work_Card.Wait_For_Element_Visibile(driver, 80, "SD_imgCancel2");
	  Work_Card.Wait_For_Element_Stalenes(driver, "btnRefreshOvedDetails");
	  Work_Card.Cancel_Schedule_02(driver).click();
	  Work_Card.Wait_For_Element_Visibile(driver, 60, "btnUpdateCard");
	  //Work_Card.Wait_For_Element_Stalenes(driver, "btnUpdateCard");
	  Work_Card.Btn_Update(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver,"btnCloseCard");
	  Work_Card.Btn_Close(driver).click();
	  
	  
	  
	  
  }
  
  
  
  
  

  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=getDriver();
		//  driver=Base.Initialize_browser();
		//  Base.Initialize_Webpage(driver);
		Utils.Enter_Workcard(driver);
	  
	  
	  
  }

}
