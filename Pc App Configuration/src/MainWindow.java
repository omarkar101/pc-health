import java.awt.BorderLayout;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.border.EmptyBorder;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;

import org.codehaus.jackson.JsonParseException;
import org.codehaus.jackson.map.JsonMappingException;

import javax.swing.JTextField;
import javax.swing.JLabel;
import java.awt.Font;
import javax.swing.JComboBox;
import javax.swing.JComponent;
import javax.swing.JList;
import javax.swing.JOptionPane;
import javax.swing.JButton;
import java.awt.event.ActionListener;
import java.io.IOException;
import java.util.List;
import java.util.Vector;
import java.awt.event.ActionEvent;
import javax.swing.AbstractListModel;
import javax.swing.DefaultListModel;
import javax.swing.JScrollPane;

public class MainWindow extends JFrame {

	private JPanel contentPane;
	private JTextField ContactEmailTxtBx;
	private JTextField PcUsernameTxtBx;
	private JLabel PcUsernameLbl;
	private JLabel ContactEmailLbl;
	private JLabel lblNewLabel_2;
	private JLabel AdminsLbl;
	private JLabel AdminUsernameLbl;
	private JLabel AdminPwdLbl;
	private JTextField AdminUsernameTxtBx;
	private JTextField AdminPwdTxtBx;
	private JButton EditAdminButton;
	private JButton OkButton;
	private JButton CancelButton;
	private ConfigurationModel Configurations;
	private Vector<Tuple> listVector;

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					MainWindow frame = new MainWindow();
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the frame.
	 * 
	 * @throws IOException
	 * @throws JsonMappingException
	 * @throws JsonParseException
	 */
	public MainWindow() throws JsonParseException, JsonMappingException, IOException {
		listVector = new Vector<Tuple>();
		this.Configurations = ConfigFileParser.Deserialize();
		listVector.addAll(Configurations.Admins);
		setTitle("Config App");
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 657, 582);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);

		ContactEmailTxtBx = new JTextField();
		ContactEmailTxtBx.setBounds(155, 27, 273, 28);
		contentPane.add(ContactEmailTxtBx);
		ContactEmailTxtBx.setColumns(10);
		ContactEmailTxtBx.setText(Configurations.PcEmail);

		PcUsernameTxtBx = new JTextField();
		PcUsernameTxtBx.setBounds(155, 83, 273, 28);
		contentPane.add(PcUsernameTxtBx);
		PcUsernameTxtBx.setColumns(10);
		PcUsernameTxtBx.setText(Configurations.PcUsername);

		PcUsernameLbl = new JLabel("Pc Username");
		PcUsernameLbl.setBounds(26, 79, 154, 24);
		PcUsernameLbl.setFont(new Font("Tahoma", Font.PLAIN, 18));
		contentPane.add(PcUsernameLbl);

		ContactEmailLbl = new JLabel("Contact Email");
		ContactEmailLbl.setBounds(26, 27, 119, 33);
		ContactEmailLbl.setFont(new Font("Tahoma", Font.PLAIN, 18));
		contentPane.add(ContactEmailLbl);

		lblNewLabel_2 = new JLabel(
				"____________________________________________________________________________________________________________________________________________________________________________________-");
		lblNewLabel_2.setBounds(0, 152, 653, 24);
		contentPane.add(lblNewLabel_2);

		AdminsLbl = new JLabel("Admins");
		AdminsLbl.setBounds(35, 174, 125, 33);
		AdminsLbl.setFont(new Font("Tahoma", Font.PLAIN, 20));
		contentPane.add(AdminsLbl);

		AdminUsernameLbl = new JLabel("Admin Username");
		AdminUsernameLbl.setBounds(309, 208, 138, 33);
		AdminUsernameLbl.setFont(new Font("Tahoma", Font.PLAIN, 16));
		contentPane.add(AdminUsernameLbl);

		AdminPwdLbl = new JLabel("Admin Password");
		AdminPwdLbl.setBounds(309, 252, 138, 33);
		AdminPwdLbl.setFont(new Font("Tahoma", Font.PLAIN, 16));
		contentPane.add(AdminPwdLbl);

		AdminUsernameTxtBx = new JTextField();
		AdminUsernameTxtBx.setEditable(false);
		AdminUsernameTxtBx.setBounds(458, 216, 175, 24);
		AdminUsernameTxtBx.setFont(new Font("Tahoma", Font.PLAIN, 14));
		contentPane.add(AdminUsernameTxtBx);
		AdminUsernameTxtBx.setColumns(10);

		AdminPwdTxtBx = new JTextField();
		AdminPwdTxtBx.setEditable(false);
		AdminPwdTxtBx.setBounds(458, 260, 175, 25);
		AdminPwdTxtBx.setFont(new Font("Tahoma", Font.PLAIN, 14));
		contentPane.add(AdminPwdTxtBx);
		AdminPwdTxtBx.setColumns(10);

		JScrollPane AdminScrollPane = new JScrollPane();
		AdminScrollPane.setBounds(26, 216, 261, 275);
		contentPane.add(AdminScrollPane);

		JList<Tuple> AdminList = new JList<Tuple>();
		AdminList.setModel(new AbstractListModel() {
			String[] values = new String[] {};

			public int getSize() {
				return values.length;
			}

			public Object getElementAt(int index) {
				return values[index];
			}
		});
		AdminScrollPane.setViewportView(AdminList);
		AdminList.setListData(listVector);
		AdminList.addListSelectionListener(new ListSelectionListener() {

			@Override
			public void valueChanged(ListSelectionEvent e) {
				if (AdminList.getSelectedValue() != null) {
					AdminUsernameTxtBx.setText(AdminList.getSelectedValue().Item1);
					AdminPwdTxtBx.setText(AdminList.getSelectedValue().Item2);
				}
			}
		});

		JButton AddAdminButton = new JButton("+");
		AddAdminButton.setBounds(26, 510, 50, 35);
		AddAdminButton.setFont(new Font("Tahoma", Font.BOLD, 16));
		AddAdminButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				JTextField AdminUsername = new JTextField();
				JTextField AdminPassword = new JTextField();
				final JComponent[] inputs = new JComponent[] { new JLabel("Email"), AdminUsername,
						new JLabel("Password"), AdminPassword };
				int result = JOptionPane.showConfirmDialog(null, inputs, "add admin", JOptionPane.PLAIN_MESSAGE);
				if (result == JOptionPane.OK_OPTION) {
					Tuple t = new Tuple();
					t.Item1 = AdminUsername.getText();
					t.Item2 = AdminPassword.getText();
					listVector.add(t);
					AdminList.setListData(listVector);
				}
				AdminList.setListData(listVector);
			}
		});
		contentPane.add(AddAdminButton);

		JButton RemoveAdminButton = new JButton("-");
		RemoveAdminButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				Tuple t = AdminList.getSelectedValue();
				listVector.remove(t);
				AdminList.setListData(listVector);
			}
		});
		RemoveAdminButton.setBounds(86, 510, 50, 35);
		RemoveAdminButton.setFont(new Font("Tahoma", Font.BOLD, 20));
		contentPane.add(RemoveAdminButton);

		EditAdminButton = new JButton("Edit");
		EditAdminButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				Tuple selectedTuple = AdminList.getSelectedValue();
				JTextField AdminUsername = new JTextField();
				JTextField AdminPassword = new JTextField();
				AdminUsername.setText(selectedTuple.Item1);
				AdminPassword.setText(selectedTuple.Item2);
				final JComponent[] inputs = new JComponent[] { new JLabel("Email"), AdminUsername,
						new JLabel("Password"), AdminPassword };
				int result = JOptionPane.showConfirmDialog(null, inputs, "edit admin", JOptionPane.PLAIN_MESSAGE);
				if (result == JOptionPane.OK_OPTION) {
					Tuple temp = new Tuple();
					temp.Item1 = AdminUsername.getText();
					temp.Item2 = AdminPassword.getText();
					int index = listVector.indexOf(selectedTuple);
					listVector.set(index, temp);
					AdminList.setListData(listVector);
				}
				AdminList.setListData(listVector);
			}
		});
		EditAdminButton.setBounds(210, 510, 89, 35);
		EditAdminButton.setFont(new Font("Tahoma", Font.PLAIN, 19));
		contentPane.add(EditAdminButton);

		OkButton = new JButton("Ok");
		OkButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				Configurations.PcEmail = ContactEmailTxtBx.getText();
				Configurations.PcUsername = PcUsernameTxtBx.getText();
				Configurations.Admins = (List<Tuple>) listVector;
				try {
					ConfigFileParser.WriteToFile(ConfigFileParser.Serialize(Configurations));
				} catch (IOException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
				System.exit(0);
			}
		});
		OkButton.setBounds(442, 510, 89, 33);
		OkButton.setFont(new Font("Tahoma", Font.PLAIN, 18));
		contentPane.add(OkButton);

		CancelButton = new JButton("Cancel");
		CancelButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				System.exit(0);
			}
		});
		CancelButton.setBounds(544, 510, 89, 35);
		CancelButton.setFont(new Font("Tahoma", Font.PLAIN, 18));
		contentPane.add(CancelButton);

	}
}
