import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Scanner;

import org.codehaus.jackson.JsonGenerationException;
import org.codehaus.jackson.JsonParseException;
import org.codehaus.jackson.map.JsonMappingException;
import org.codehaus.jackson.map.ObjectMapper;

public class ConfigFileParser {
	public static ConfigurationModel Deserialize() throws JsonParseException, JsonMappingException, IOException {
		String jsonString = readFile();
		ObjectMapper objectmapper = new ObjectMapper();
		ConfigurationModel model = objectmapper.readValue(jsonString, ConfigurationModel.class);
		return model;
	}
	public static String Serialize(ConfigurationModel m) throws JsonGenerationException, JsonMappingException, IOException {
		ObjectMapper objectmapper = new ObjectMapper();
		String jsonString = objectmapper.writeValueAsString(m);
		return jsonString;
	}
	public static void WriteToFile(String s) throws IOException {
		String userDirectory = System.getProperty("user.dir");
		String os = System.getProperty("os.name");
		File f;
		if(os.toLowerCase().charAt(0) == 'w')
		{
			f = new File(userDirectory + "\\Configurations.json");
		}
		else
		{
			f = new File(userDirectory + "/Configurations.json");
		}
		FileWriter writer = new FileWriter(f);
		writer.write(s);
		writer.close();
	}
	public static String readFile() {
		StringBuilder JsonFileString = new StringBuilder(); 
		try {
			String userDirectory = System.getProperty("user.dir");
			String os = System.getProperty("os.name");
			File myObj;
			if(os.toLowerCase().charAt(0) == 'w')
			{
				myObj = new File(userDirectory + "\\Configurations.json");
			}
			else
			{
				myObj = new File(userDirectory + "/Configurations.json");
			}
			Scanner myReader = new Scanner(myObj);
			while (myReader.hasNextLine()) {
				String data = myReader.nextLine();
				JsonFileString.append(data);
			}
			myReader.close();
			return JsonFileString.toString();
		} catch (Exception e) {
			System.out.println(e);
			return "";
		}
	}
}
