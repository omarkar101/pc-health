import java.util.List;
public class ConfigurationModel {
	public String PcUsername;
	public String PcEmail;
	@Override
	public String toString() {
		return "ConfigurationModel [PcUsername=" + PcUsername + ", PcEmail=" + PcEmail + ", Admins=" + Admins + "]";
	}
	public List<Tuple> Admins;
}
class Tuple{
	public String Item1;
	public String Item2;
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((Item1 == null) ? 0 : Item1.hashCode());
		result = prime * result + ((Item2 == null) ? 0 : Item2.hashCode());
		return result;
	}
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Tuple other = (Tuple) obj;
		if (Item1 == null) {
			if (other.Item1 != null)
				return false;
		} else if (!Item1.equals(other.Item1))
			return false;
		if (Item2 == null) {
			if (other.Item2 != null)
				return false;
		} else if (!Item2.equals(other.Item2))
			return false;
		return true;
	}
	@Override
	public String toString() {
		return Item1;
	}
}
