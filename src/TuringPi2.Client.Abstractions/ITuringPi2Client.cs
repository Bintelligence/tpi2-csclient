// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the Turing Pi 2 Client.
    /// </summary>
    public interface ITuringPi2Client
    {
        /// <summary>
        /// Gets the node info.
        /// </summary>
        /// <returns>a <see cref="NodeInfo"/> object containing information about the installed nodes.</returns>
        Task<NodeInfo> GetNodeInfoAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the power state of all the nodes.
        /// </summary>
        /// <returns>a <see cref="Power"/> object containing the power state of each node.</returns>
        Task<Power> GetPowerAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the state for the USB interface.
        /// </summary>
        /// <returns>a <see cref="USB"/> object to indicate to which node the USB interface is connected and which mode it is in.</returns>
        Task<USB> GetUSBAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the status of the BMC SD Card.
        /// </summary>
        /// <returns></returns>
        Task<SDCard> GetSDCardAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Changes the power of one or more nodes.
        /// </summary>
        /// <param name="powerSetting">the power settings to apply to each node.</param>
        /// <returns>a <see cref="Task"/> representing the asynchonous operation.</returns>
        Task ChangePowerAsync(CancellationToken cancellationToken = default, params NodePowerSetting[] powerSetting);

        /// <summary>
        /// Resets the network interface.
        /// </summary>
        /// <returns>a <see cref="Task"/> representing the asynchonous operation.</returns>
        Task ResetNetworkAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs an OTA-update using a "SWU" file.
        /// </summary>
        /// <remarks>
        /// See the Turing Pi 2 documentation for details about updating the firmware.
        /// </remarks>
        /// <returns>a <see cref="Task"/> representing the asynchonous operation.</returns>
        Task UpdateFirmwareAsync(string filePath, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the USB interface to the specified node & type.
        /// </summary>
        /// <param name="usb">the desired USB state.</param>
        /// <returns></returns>
        Task SetUSBAsync(USB usb, CancellationToken cancellationToken = default);
    }
}