package pageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;





public class Tickur_Chishuv_LeOved {
	
	
	
 private static WebElement element;
	 
	 public static WebElement Mispar_Ishi(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_txtEmpId"));
		 
		    return element;
		 
		    }
	
	 
	 
	 
	
	 public static WebElement Btn_Show(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_btnShow"));
		 
		    return element;
		 
		    }
	 
	 
	 

	 public static WebElement Grd_Calc_Detalils(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_pnlTotalMonthly"));
		 
		    return element;
		 
		    }
	 
	 
	 

	 public static WebElement List_Month(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_ddlMonth"));
		 
		    return element;
		 
		    }
	 
	 
	 
	 public static WebElement Btn_Print(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_ImagePrint"));
		 
		    return element;
		 
		    }
	 

}
