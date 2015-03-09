package pageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class Work_Card {
	
	
	
 private static WebElement element;
 
 
 
 public static WebElement Btn_Show (WebDriver driver){
	 
	    element = driver.findElement(By.id("btnRefreshOvedDetails"));
	 
	    return element;

}
 
 
 
 
 
 
 public static WebElement Date (WebDriver driver){
	 
	    element = driver.findElement(By.id("clnDate"));
	 
	    return element;

}
 
 
 public static WebElement TxtId (WebDriver driver){
	 
	    element = driver.findElement(By.id("txtId"));
	 
	    return element;

}
 
 
 
 
 
 
 public static WebElement Tachograph (WebDriver driver){
	 
	    element = driver.findElement(By.id("ddlTachograph"));
	 
	    return element;

   }
 
 
 
 
    public static WebElement Lina (WebDriver driver){
	 
	    element = driver.findElement(By.id("ddlLina"));
	 
	    return element;

   }
 
 
 
	 
	 public static WebElement Start_Time(WebDriver driver){
		 
		    element = driver.findElement(By.id("SD_txtSH0"));
		 
		    return element;
	
	 }
	
	
	 
	 public static WebElement End_Time(WebDriver driver){
		 
		    element = driver.findElement(By.id("SD_txtSG0"));
		 
		    return element;
	
	 }
	
	 
	 public static WebElement Entry_Time(WebDriver driver){
		 
		    element = driver.findElement(By.id("SD_000_ctl03_SD_000_ctl03ShatYetiza"));
		 
		    return element;
	
	 }
	
	 
	 public static WebElement Car_Num(WebDriver driver){
		 
		    element = driver.findElement(By.id("SD_000_ctl03_SD_000_ctl03CarNumber"));
		 
		    return element;
	
	 }
	 
	 
	 
	 
	
	 public static WebElement Btn_Update(WebDriver driver){
		 
		    element = driver.findElement(By.id("btnUpdateCard"));
		 
		    return element;
	
	 }
	 
	 
	 
	 

}