package pageObjects;

import java.util.concurrent.TimeUnit;
import org.openqa.selenium.By;
import org.openqa.selenium.ElementNotVisibleException;
import org.openqa.selenium.StaleElementReferenceException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.FluentWait;
import org.openqa.selenium.support.ui.WebDriverWait;

import com.google.common.base.Function;

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
 
 
 
 
 public static WebElement Btn_Next_Day (WebDriver driver){
	 
	    element = driver.findElement(By.id("btnNextCard"));
	 
	    return element;

}
 
 
 
 
 
 public static WebElement Btn_Prev_Day (WebDriver driver){
	 
	    element = driver.findElement(By.id("btnPrevCard"));
	 
	    return element;

}

 
 
 
 
 
 public static WebElement Day_Plus (WebDriver driver){
	 
	    element = driver.findElement(By.id("btnPlus2"));
	 
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
 
 
    public static WebElement Hamara (WebDriver driver){
   	 
	    element = driver.findElement(By.id("btnHamara"));
	 
	    return element;

   }
    
    
    
    
    public static WebElement Halbasha (WebDriver driver){
      	 
	    element = driver.findElement(By.id("ddlHalbasha"));
	 
	    return element;

   }
    
    
    
    public static WebElement HashlamaForDay (WebDriver driver){
     	 
	    element = driver.findElement(By.id("btnHashlamaForDay"));
	 
	    return element;

   }
    
    
    
    
    public static WebElement HashlamaReason (WebDriver driver){
    	 
	    element = driver.findElement(By.id("ddlHashlamaReason"));
	 
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
	 
	 
	 
	 public static WebElement Assert_Car_Num(WebDriver driver){
		 
		    element = driver.findElement(By.id("SD_000_ctl04_SD_000_ctl04CarNumber"));
		 
		    return element;
	
	 }
	 
	 
	 
	 
	 public static WebElement Btn_No_Copy_Car_Num(WebDriver driver){
		 
		    element = driver.findElement(By.id("btnNo"));
		 
		    return element;
	
	 }
	 
	 
	 
	 public static WebElement Btn_Yes_Copy_Car_Num(WebDriver driver){
		 
		    element = driver.findElement(By.id("btnYes"));
		 
		    return element;
	
	 }
	 
	 
	 
	 
	 public static WebElement Validate_Popup(WebDriver driver){
		 
		    element = driver.findElement(By.className("ajax__validatorcallout_error_message_cell"));
		 
		    return element;
	
	 }
	 
	 
	 
	 
	
	 public static WebElement Btn_Update(WebDriver driver){
		 
		    element = driver.findElement(By.id("btnUpdateCard"));
		 
		    return element;
	
	 }
	 
	 
	 

	 
	 
	 public static WebElement Btn_Cancel_Update(WebDriver driver){
		 
		    element = driver.findElement(By.id("btnCancel"));
		    
		    
		    return element;
	
	 }

	 
	 
	 
	 
		
	 public static WebElement Btn_Close(WebDriver driver){
		 
		    element = driver.findElement(By.id("btnCloseCard"));
		    
		 
		    return element;
	
	 }
	 
	 
	
	
	  
		
		 public static WebElement AddRekaUp_mapa(WebDriver driver){
			 
			    element = driver.findElement(By.id("SD_000_ctl03_AddRekaUpSD_000_ctl03"));
			    
			 
			    return element;
		
		 }
	  
		 
		 
		 
		  
			
		 public static WebElement AddRekadw_mapa(WebDriver driver){
			 
			    element = driver.findElement(By.id("SD_002_ctl04_AddRekaSD_002_ctl04"));
			    
			 
			    return element;
		
		 }
		 
	  
		 
		 public static WebElement Makat_Num_Reka_Mapa(WebDriver driver){
			 
			    element = driver.findElement(By.id("SD_000_ctl03_SD_000_ctl03MakatNumber"));
			    
			 
			    return element;
		 }
			    
			    
			    
	     public static WebElement Cancel_Empty_Activity_Mapa (WebDriver driver){
					 
				  element = driver.findElement(By.id("SD_000_ctl03_SD_000_ctl03CancelPeilut"));
				    
				 
				   return element;
		
		 }
	  
	  
	     public static WebElement Assert_Reka_Between_Not_Found (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_001_ctl03_AddRekaSD_001_ctl03"));
			    
			 
			   return element;  }
			   
			   
	     
	     
		public static WebElement Assert_Reka_Between_Not_Able (WebDriver driver){
					 
			 element = driver.findElement(By.id("SD_000_ctl11_AddRekaSD_000_ctl11"));
					    
					 
		      return element;
			   
	
	 }
	     
	     
		
		
		public static WebElement Add_Reka_Between (WebDriver driver){
			 
			 element = driver.findElement(By.id("SD_002_ctl03_AddRekaSD_002_ctl03"));
					    
					 
		      return element;
			   
	
	 }
	     
	 
		
		public static WebElement Cancel__Empty_Activity_Between  (WebDriver driver){
			 
			 element = driver.findElement(By.id("SD_002_ctl05_SD_002_ctl05CancelPeilut"));
					    
					 
		      return element;
			   
	
	 }
		
		
		 public static WebElement Makat_Num_Reka_Between (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_002_ctl05_SD_002_ctl05MakatNumber"));
			    
			 
			   return element;  }
		 
		 
		 
		 public static WebElement  Cancel_Sidur (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_imgCancel0"));
			    
			 
			   return element;  }
		
		 
		 
		 public static WebElement  Lbl_Sidur_Num_0 (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_lblSidur0"));
			    
			 
			   return element;  }
		 
		 
		 public static WebElement  Btn_Add_Absence (WebDriver driver){
			 
			  element = driver.findElement(By.id("btnAddHeadrut"));
			    
			 
			   return element;  }
		 
		 
		 
		 
	
		 
		 
		 public static WebElement  Btn_Add_Activity (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_imgAddPeilut0"));
			    
			 
			   return element;  }
		 
		 
		 
		 
		 public static WebElement  Entry_Time_Add_Activity (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_000_ctl08_SD_000_ctl08ShatYetiza"));
			    
			 
			   return element;  }
		 
		 
		 
		 public static WebElement  Makat_Num_Add_Activity (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_000_ctl08_SD_000_ctl08MakatNumber"));
			    
			 
			   return element;  }
		 
		 
		 
		 
		 public static WebElement  Car_Num_Add_Activity (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_000_ctl08_SD_000_ctl08CarNumber"));
			    
			 
			   return element;  }
		 
		 
		 
		 public static WebElement  Cancel_Sidur_Add_Activity (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_000_ctl08_SD_000_ctl08CancelPeilut"));
			    
			 
			   return element;  }
		
		 
		
		 
		 public static WebElement  Assert_Activity_Car_No (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_000_ctl08_SD_000_ctl08CarNumber"));
			    
			 
			   return element;  }
		 
		 
		 
		 
		 public static WebElement  Btn_Add_Special (WebDriver driver){
			 
			  element = driver.findElement(By.id("btnAddMyuchad"));
			    
			 
			   return element;  }
		 
		 
		 
		 public static WebElement  Lbl_Sidur_No_2 (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_lblSidur2"));
			    
			 
			   return element;  }
		 
		 
		 public static WebElement  Lbl_Special_No_Meshek (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_lblSidur0"));
			    
			 
			   return element;  }
		 
		 
		 
		 
		 public static WebElement  Assert_Sidur_disabled (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_txtSH1"));
			    
			 
			   return element;  }
		 
		 
		 
		 public static WebElement  List_Reason_In (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_ddlResonIn2"));
			    
			 
			   return element;  }
		 
		 
		 
		 public static WebElement  List_Reason_Out (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_ddlResonOut2"));
			    
			 
			   return element;  }
		 
		 
		 
		 public static WebElement  Start_Time_Special (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_txtSH2"));
			    
			 
			   return element;  }
		 
		 
		 
		 
		 public static WebElement  End_Time_Special (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_txtSG2"));
			    
			 
			   return element;  }
		 
		 
		 
		 
		 public static WebElement  Cancel_Schedule_02 (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_imgCancel2"));
			    
			 
			   return element;  }
		 
		 
		 
		 public static WebElement  Add_Activity_Special (WebDriver driver){
			 
			  element = driver.findElement(By.id("SD_imgAddPeilut2"));
			    
			 
			   return element;  }
		 
		 
		 
		 
		 
		 public static WebElement  Btn_Add_Mapa (WebDriver driver){
			 
			  element = driver.findElement(By.id("btnFindSidur"));
			    
			 
			   return element;  }
		 
		 
		 
		 
		 public static WebElement  Btn_Calc_Items (WebDriver driver){
			 
			  element = driver.findElement(By.id("btnCalcItem"));
			    
			 
			   return element;  }
		 
		 
		
		 
		 public static WebElement  Btn_Driver_Errors (WebDriver driver){
			 
			  element = driver.findElement(By.id("btnDrvErrors"));
			    
			 
			   return element;  }
		 
		 
		 
		
		 public static WebElement  Btn_Clocks (WebDriver driver){
			 
			  element = driver.findElement(By.id("btnClock"));
			    
			 
			   return element;  }
		 
		 
		 
		 
		 
		 public static WebElement Wait_For_Element_Visibile(final WebDriver driver, final int timeoutSeconds,String snameId) {
			    FluentWait<WebDriver> wait = new FluentWait<WebDriver>(driver)
			    		
			            .withTimeout(timeoutSeconds, TimeUnit.SECONDS)
			            .pollingEvery(500, TimeUnit.MILLISECONDS)
			            .ignoring(StaleElementReferenceException.class,ElementNotVisibleException.class);
			            
			    return wait.until(new Function<WebDriver, WebElement>() {
			        public WebElement apply(WebDriver webDriver) {
			        	WebDriverWait wait = new WebDriverWait(driver,50);
			        	wait.until(ExpectedConditions.visibilityOf(driver.findElement(By.id(snameId))));
			        	element = driver.findElement(By.id(snameId));
			        	System.out.println("Trying to find element " + element.toString()); 
			            return element;
			        }
			    });
			}
		 
		 
			

		 
		 
		 


		 
		 public static WebElement Wait_For_Element_Stalenes( WebDriver driver,String snameId ,String  sclassname  ) {
			   
			            if  (snameId!=null) 
			            		{		
			        	WebDriverWait wait = new WebDriverWait(driver,120);
			        	wait.until(ExpectedConditions.stalenessOf((driver.findElement(By.id(snameId)))));} 
			        	
			            else 
			            	
			            {
			            	WebDriverWait wait1 = new WebDriverWait(driver,120);
			        	wait1.until(ExpectedConditions.stalenessOf((driver.findElement(By.className(sclassname)))));}
			            	
			        	
			            return element;
			        }
			  
		 
		 
		 
	
		 
		 
		 
		 
		 
		 
			}
	
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		


