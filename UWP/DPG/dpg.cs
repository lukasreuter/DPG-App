using DPG;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.ApplicationModel.DataTransfer;

public class dpg
{
	private static int _iterations = 32768;
	private static int _keyLengthInBytes = 64;

	private static char[] _hexArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

	// This is a FIFO data structure similar to std::queue in C++
	private Queue<int> _numberQueue = new Queue<int>();

	/**
    * Method to make passwords easy for humans to read
    **/
	private string Readable(string password)
	{
		var x = 0;
		var str = "";
		while (x < password.Length)
		{
			str += password.Substring(x, 3) + " ";
			x += 3;
		}
		return str;
	}

	/**
    * Convert byte[] to hex string
    * https://stackoverflow.com/questions/9655181/how-to-convert-a-byte-array-to-a-hex-string-in-java
    **/
	private string ConvertByteArrayToHex(byte[] bytes)
	{
		var hexChars = new char[bytes.Length * 2];

		for (var j = 0; j < bytes.Length; j++)
		{
			var v = bytes[j] & 0xFF;
			hexChars[j * 2] = _hexArray[v >> 4];
			hexChars[j * 2 + 1] = _hexArray[v & 0x0F];
		}

		return new string(hexChars);
	}

	/**
     * A method to get 64 random bytes deterministically based on user input
     * And to also fill the queue with numbers
     **/
	private Queue<int> CreateCryptoNumbers(ref string sentence, ref string word)
	{
		var sentenceBytes = Encoding.UTF8.GetBytes(sentence);
		var wordBytes = Encoding.UTF8.GetBytes(word);
		var pwd = KeyGen.PBKDF2Sha256GetBytes(_keyLengthInBytes * 8, sentenceBytes, wordBytes, _iterations);

		// remove all traces of the secrets
		sentence = string.Empty;
		word = string.Empty;
		Array.Clear(sentenceBytes, 0, sentenceBytes.Length);
		Array.Clear(wordBytes, 0, wordBytes.Length);

		DebugPrint("PBKDF2:  " + Encoding.UTF8.GetString(pwd));

		var queue = new Queue<int>();
		foreach (var b in pwd)
		{
			// & 0xFF ensures the int is unsigned
			var letter = b & 0xFF;

			DebugPrint(letter.ToString());
			queue.Enqueue(letter);
		}
		return queue;
	}

	[Conditional("DEBUG")]
	private void DebugPrint(string str)
	{
		//TODO: implement this
	}

	/**
     * The GUI
     */
	public dpg()
	{
		//JButton btnBig = new JButton("Big");
		//JButton btnSmall = new JButton("Small");
		//JButton btnPhrase = new JButton("Passphrase");
		//JButton btnSee = new JButton("See it");
		//JButton btnClr = new JButton(" ");

		//btnClr.setToolTipText("Pick colors to distinguish multiple instances of DPG.");
		//txtPeek.setToolTipText("See the first few characters of the password or passphrase.");
		//btnBig.setToolTipText("Generate a big password.");
		//btnSmall.setToolTipText("Generate a small password.");
		//btnPhrase.setToolTipText("Generate a passphrase.");
		//btnSee.setToolTipText("See a readable version of the password or passphrase.");
		//txtSentence.setToolTipText("Enter a sentence");
		//txtWord.setToolTipText("Enter a word");

		//JCheckBox chkSecure = new JCheckBox("Secure");
		//JButton btnView = new JButton("View");

		//chkSecure.setToolTipText("Prevent shoulder surfing and tampering.");
		//btnView.setToolTipText("View the sentence and the word.");

		//JLabel lblSentence = new JLabel("Sentence");
		//JLabel lblWord = new JLabel("Word");
		//lblSentence.setToolTipText("Enter a sentence");
		//lblWord.setToolTipText("Enter a word");
	}

	public void GeneratePassword(string sentence, string word)
	{
		var queue = CreateCryptoNumbers(ref sentence, ref word);

		var result = string.Empty;
		while (result.Length < (24 - 3))
		{
			result += Jars.Lower()[queue.Dequeue()];
		}
		result += Jars.Special()[queue.Dequeue()];
		result += Jars.Upper()[queue.Dequeue()];
		result += Jars.Numbers()[queue.Dequeue()];

		// destroy all traces of the secret
		queue.Clear();

		// Copy to Clipboard
		var dataPackage = new DataPackage
		{
			RequestedOperation = DataPackageOperation.Copy
		};
		dataPackage.SetText(result);
		Clipboard.SetContent(dataPackage);
	}
	/*
	public void actionPerformed(ActionEvent e)
	{
		string sentence = txtSentence.getPassword();
		string word = txtWord.getPassword();
		var result = "";
		_numberQueue.Clear();

		if (sentence.Length == 0)
		{
			showMessageDialog("The sentence is empty.", "Error", JOptionPane.ERROR_MESSAGE);
			return;
		}

		if (word.Length == 0)
		{
			showMessageDialog("The word is empty.", "Error", JOptionPane.ERROR_MESSAGE);
			return;
		}

		CreateCryptoNumbers(sentence, word);

		const int length = (12 - 3);
		for (var i = 0; i < length; ++i)
		{
			result += Jars.Lower()[_numberQueue.Dequeue()];
		}
		while (result.Length < (12 - 3))
		{
			result += Jars.Lower()[_numberQueue.Dequeue()];
		}
		result += Jars.Special()[_numberQueue.Dequeue()];
		result += Jars.Upper()[_numberQueue.Dequeue()];
		result += Jars.Numbers()[_numberQueue.Dequeue()];

		txtPeek.setText(result.substring(0, 4));
		StringSelection stringSelection = new StringSelection(result);
		Clipboard clpbrd = Toolkit.getDefaultToolkit().getSystemClipboard();
		clpbrd.setContents(stringSelection, null);
	}

	public void actionPerformed(ActionEvent e)
	{
		var sentence = txtSentence.getPassword();
		var word = txtWord.getPassword();
		var result = "";
		_numberQueue.Clear();

		if (sentence.Length == 0)
		{
			JOptionPane.showMessageDialog(cp, "The sentence is empty.", "Error", JOptionPane.ERROR_MESSAGE);
			return;
		}

		if (word.Length == 0)
		{
			JOptionPane.showMessageDialog(cp, "The word is empty.", "Error", JOptionPane.ERROR_MESSAGE);
			return;
		}

		CreatePassword(sentence, word);

		var i = 0;
		while (i < (10 - 3))
		{
			result += j.words().get(number_queue.remove());
			i++;
		}
		result += j.special().get(number_queue.remove());
		result += j.upper().get(number_queue.remove());
		result += j.numbers().get(number_queue.remove());

		txtPeek.setText(result.substring(0, 4));
		StringSelection stringSelection = new StringSelection(result);
		Clipboard clpbrd = Toolkit.getDefaultToolkit().getSystemClipboard();
		clpbrd.setContents(stringSelection, null);
	}

	public void actionPerformed(ActionEvent e)
	{
		String sentenceText = new String(txtSentence.getPassword());
		String wordText = new String(txtWord.getPassword());
		human = Readable(result);
		JOptionPane.showMessageDialog(cp, "Password:  " + human);
	}

	

	public void actionPerformed(ActionEvent e)
	{
		if (btnView.isEnabled())
		{
			btnView.setSelected(false);
			btnView.setEnabled(false);
		}
		else
		{
			btnView.setEnabled(true);
			txtPeek.setText("");
			txtSentence.setText("");
			txtWord.setText("");
			human = "";
			result = "";

			StringSelection stringSelection = new StringSelection("");
			Clipboard clpbrd = Toolkit.getDefaultToolkit().getSystemClipboard();
			clpbrd.setContents(stringSelection, null);
		}
	}

	public void actionPerformed(ActionEvent e)
	{
		String sentenceText = new String(txtSentence.getPassword());
		String wordText = new String(txtWord.getPassword());
		JOptionPane.showMessageDialog(cp, "Sentence: " + sentenceText + "\n" + "Word: " + wordText);
	}
	*/
}
