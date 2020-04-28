using FastFrame.Dto.Dtos.Chat;
using FastFrame.Repository;
using FastFrame.Infrastructure;
using System;
using System.Threading.Tasks;
using FastFrame.Infrastructure.Interface;
using FastFrame.Entity.Chat;
using FastFrame.Infrastructure.MessageBus;
using MsgType = FastFrame.Infrastructure.MessageBus.MsgType;

namespace FastFrame.Service.Services.Chat
{
    /// <summary>
    /// 好友消息
    /// </summary>
    public partial class ChatService : IService
    {
        private readonly IAppSessionProvider appSession;
        private readonly IRepository<FriendMessage> friendMsgRepository;
        private readonly IRepository<MessageTarget> msgTgRepository;
        private readonly IMessageBus messageBus;

        public ChatService(IAppSessionProvider appSession,
            IRepository<FriendMessage> friendMsgRepository,
            IRepository<MessageTarget> msgTgRepository,
            IMessageBus messageBus)
        {
            this.appSession = appSession;
            this.friendMsgRepository = friendMsgRepository;
            this.msgTgRepository = msgTgRepository;
            this.messageBus = messageBus;
        }

        /// <summary>
        /// 发送消息
        /// </summary> 
        public async Task<RecMsgOutPut> SendFriendMsg(FriendMsgInput input)
        {
            /*保存消息*/
            var entity = input.MapTo<FriendMsgInput, FriendMessage>();
            entity.From_Id = appSession.CurrUser?.Id;
            entity.MessageTime = DateTime.Now;
            entity = await friendMsgRepository.AddAsync(entity);

            /*保存消息接收人*/
            await msgTgRepository.AddAsync(new MessageTarget()
            {
                Message_Id = entity.Id,
                To_Id = input.Target_User_Id
            });

            await friendMsgRepository.CommmitAsync();

            /*通知接收人*/
            var outPut = input.MapTo<FriendMsgInput, RecMsgOutPut>();
            outPut.Id = entity.Id;
            outPut.From_User_Id = entity.From_Id;

            await messageBus.PubLishAsync(new Message<RecMsgOutPut>(MsgType.FriendMsg, outPut)
            {
                Target_Ids = new string[] { input.Target_User_Id }
            });

            return outPut;
        } 
    }
}
