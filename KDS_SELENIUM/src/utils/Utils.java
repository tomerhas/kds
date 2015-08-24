package utils;

import java.io.File;
import java.util.Set;
import java.util.concurrent.TimeUnit;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.openqa.selenium.By;
import org.openqa.selenium.NoSuchWindowException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.Select;

import pageObjects.Employee_Card;
import pageObjects.LogIn_Page;

public  class Utils {

//	public  WebDriver driver;
	
	 int attempts =0;
	 int MAX_ATTEMPTS =40;
	
	
	public static WebDriver Initialize_browser( ) {
		
		
		File file = new File("C:/Selenium/IEDriverServer.exe");
		System.setProperty("webdriver.ie.driver", file.getAbsolutePath());
		return  new InternetExplorerDriver();
		 
		 	
	}
	
    
   public static  void  Initialize_Webpage(WebDriver driver) {
	   
	   
	   
		
	//   Runtime.getRuntime().exec("C:\\selenium\\workspace\\autotest.exe");
	   
	   driver.navigate().to("http://kdstest");
	   //driver.get("http://igalr:DD2468@kdstest");
	   
	
	  
		 	
	}
	  

   
 public static  void  Enter_Workcard(WebDriver driver) {
	   

	  LogIn_Page.lnk_EmployeeCards(driver).click();
      String innerTitle = driver.getTitle();
      System.out.println(innerTitle);
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  Select droplist = new Select(Employee_Card.List_Month(driver));
      droplist.selectByVisibleText("03/2015"); 
      Employee_Card.Btn_Execute(driver).click();
      Employee_Card.Link_Date(driver).click();
	   
	  
		 	
	}
   

 public  void waitForWindow(String regex,WebDriver driver) {
	
	 try{
		 Set<String> windows = driver.getWindowHandles();
		 System.out.println(windows);
	
	
	for (String window : windows) {
         try {
             driver.switchTo().window(window);

             Pattern p = Pattern.compile(regex);
             Matcher m = p.matcher(driver.getCurrentUrl());

             if (m.find()) {
                 attempts = 0;
                 switchToWindow(regex,driver);
                 return;
             }
             else {
                 // try for title
                 m = p.matcher(driver.getTitle());
                
                // if (driver.getCurrentUrl().indexOf("WorkCard")>-1){
                 if (m.find()) {
                     attempts = 0;
                      switchToWindow(regex,driver);
                      return;
                 }
             }
         } catch(NoSuchWindowException e) {
             if (attempts <= MAX_ATTEMPTS) {
                 attempts++;

                 try {Thread.sleep(1);}catch(Exception x) { x.printStackTrace(); }

                  waitForWindow(regex,driver);
                  return;
             } else {
                 fail("Window with url|title: " + regex + " did not appear after " + MAX_ATTEMPTS + " tries. Exiting.");
             }
         }
     }

	
	
     // when we reach this point, that means no window exists with that title..
     if (attempts == MAX_ATTEMPTS) {
         fail("Window with title: " + regex + " did not appear after 5 tries. Exiting.");
         return;
     } else {
         System.out.println("#waitForWindow() : Window doesn't exist yet. [" + regex + "] Trying again. " + attempts + "/" + MAX_ATTEMPTS);
         attempts++;
       waitForWindow(regex,driver);
       return ;
     }
     
 } catch (NullPointerException  e )
	 
	 {
		 System.out.print("NullPointerException caught");
	 }
     
 }
 
 


 
 
 
 public  void switchToWindow(String regex,WebDriver driver) {
     Set<String> windows = driver.getWindowHandles();

     for (String window : windows) {
         driver.switchTo().window(window);
         System.out.println(String.format("#switchToWindow() : title=%s ; url=%s",
                 driver.getTitle(),
                 driver.getCurrentUrl()));

         Pattern p = Pattern.compile(regex);
         Matcher m = p.matcher(driver.getTitle());

         if (m.find()) return ;
         else {
             m = p.matcher(driver.getCurrentUrl());
             if (m.find()) return ;
         }
     }

     
     fail("Could not switch to window with title / url: " + regex);
     return ;
 }


private  void fail(String string) {
	System.out.println(string);
	
}

public static int GetNumWindows(WebDriver driver)
{
	 return driver.getWindowHandles().size();
	 
	 
}






public void  Click_Sub_Menu  (String  menu ,  String sub_menu , WebDriver driver)   {


	 WebElement element = driver.findElement(By.linkText(menu));

     Actions action = new Actions(driver);

     action.moveToElement(element).build().perform();
     

     WebElement subElement = driver.findElement(By.linkText(sub_menu));
     
     subElement.click();

     
	



}

}


