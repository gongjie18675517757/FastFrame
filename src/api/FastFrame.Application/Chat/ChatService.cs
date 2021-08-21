using FastFrame.Entity.Chat;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using System;
using System.Threading.Tasks;

namespace FastFrame.Application.Chat
{
    /// <summary>
    /// 好友消息
    /// </summary>
    public partial class ChatService : IService
    {
        private readonly IApplicationSession appSession;
        private readonly IRepository<FriendMessage> friendMsgRepository;
        private readonly IRepository<MessageTarget> msgTgRepository;


        public ChatService(IApplicationSession appSession,
            IRepository<FriendMessage> friendMsgRepository,
            IRepository<MessageTarget> msgTgRepository
             )
        {
            this.appSession = appSession;
            this.friendMsgRepository = friendMsgRepository;
            this.msgTgRepository = msgTgRepository;

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

            return outPut;
        }
    }
}
