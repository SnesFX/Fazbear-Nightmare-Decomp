using UnityEngine;

[FriendlyName("Get AudioClip Info", "Returns AudioClip properties of the target AudioClip variable.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Audio")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Returns AudioClip properties of the target AudioClip variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GetAudioClipInfo : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject containing the animtion clip.")] AudioClip target, [FriendlyName("Length", "The length of the audio clip in seconds.")] out float clipLength, [SocketState(false, false)][FriendlyName("Samples", "The length of the audio clip in samples.")] out float clipSamples, [SocketState(false, false)][FriendlyName("Channels", "Returns the number of channels the audio clip has. 1 = Mono, 2+ = Stereo.")] out int clipChannels, [SocketState(false, false)][FriendlyName("Frequency", "The frequency of the audio clip in Hz.")] out float clipFrequency, [SocketState(false, false)][FriendlyName("Is Ready", "Returns true if the audio clip is ready to play. This is primarily used when downloading the audio clip from a web site and returns true when there is enough data downloaded to begin play without interuptions. This always returns true for audio clips not associated with a web stream.")] out bool clipIsReady)
	{
		clipLength = target.length;
		clipSamples = target.samples;
		clipChannels = target.channels;
		clipFrequency = target.frequency;
		clipIsReady = target.isReadyToPlay;
	}
}
