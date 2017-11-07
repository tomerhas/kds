package pageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class HosafatSidur {
	
	 private static WebElement element;
	
	 public static WebElement Btn_Show_Mapa(WebDriver driver){
		 
		    element = driver.findElement(By.id("btnShow"));
		 
		    return element;
		 
		    }
	 
	 
	 
	 
	 
	 public static WebElement Txt_Sidur_Mapa(WebDriver driver){
		 
		    element = driver.findElement(By.id("txtMisSidurMapa"));
		 
		    return element;
		 
		    }
	
	
	 
	 
	 public static WebElement Btn_Close_Mapa(WebDriver driver){
		 
		    element = driver.findElement(By.className("btnWorkCardCloseWin"));
		 
		    return element;
		 
		    }
	
	
	
	 
	 
	 public static WebElement Btn_Update_Mapa(WebDriver driver){
		 
		    element = driver.findElement(By.id("btnHosafa"));
		 
		    return element;
		 
		    }
	
	 
	 
	
	 public static WebElement Btn_Cheak_ALL(WebDriver driver){
		 
		    element = driver.findElement(By.id("grdPeiluyot_ctl01_lbSamenHakol"));
		 
		    return element;
		 
		    }
	
	
	
	 
	 
	 public static WebElement Btn_Clear_ALL(WebDriver driver){
		 
		    element = driver.findElement(By.id("grdPeiluyot_ctl01_lbNake"));
		 
		    return element;
		 
		    }
	 
	 
	 
	 
	 public static WebElement Validate_Popup(WebDriver driver){
		 
		    element = driver.findElement(By.className("ajax__validatorcallout_error_message_cell"));
		 
		    return element;
	
	 }
	
	
	 
	 
	 
	 public static WebElement Car_No_Mapa_1(WebDriver driver){
		 
		    element = driver.findElement(By.id("grdPeiluyot_ctl02_txtMisRechev"));
		 
		    return element;
	
	 }
	 
	 
	 
	 public static WebElement Car_No_Mapa_2(WebDriver driver){
		 
		    element = driver.findElement(By.id("grdPeiluyot_ctl03_txtMisRishuy"));
		 
		    return element;
	
	 }
	 
	 
	 
	 
	 
	 public static WebElement Chk_Activity_1(WebDriver driver){
		 
		    element = driver.findElement(By.id("grdPeiluyot_ctl02_cbHosef"));
		 
		    return element;
	
	 }
	
	 
	 
	 public static WebElement Chk_Activity_2(WebDriver driver){
		 
		    element = driver.findElement(By.id("grdPeiluyot_ctl03_cbHosef"));
		 
		    return element;
	
	 }
	 
	 
	 
	 
	 

}
